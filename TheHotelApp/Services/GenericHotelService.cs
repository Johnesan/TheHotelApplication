using TheHotelApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheHotelApp.Data;
using TheHotelApp.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ByteSizeLib;

namespace TheHotelApp.Services
{
    public class GenericHotelService<TEntity> : IGenericHotelService<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        protected DbSet<TEntity> DbSet;

        //GenericHotelService requires a context class to work. 
        //Constructor sets the DbSet property to the appropraite DbSet representing the Model Entity, ready for use.
        public GenericHotelService(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllItemsAsync()
        {
            return await DbSet.ToArrayAsync();
        }

        public async Task<TEntity> GetItemByIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }

            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.Where(expression).ToArrayAsync();
        }

        public async Task CreateItemAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditItemAsync(TEntity entity)
        {
            _context.Update(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }



        //This section contains methods particular to specific controllers
        #region Specific Controller Methods


        public RoomsAdminIndexViewModel GetAllRoomsAndRoomTypes()
        {

            var rooms = _context.Rooms.ToList();
            var roomtypes = _context.RoomTypes.ToList();

            var RoomsAdminIndeViewModel = new RoomsAdminIndexViewModel
            {
                Rooms = rooms,
                RoomTypes = roomtypes
            };
            return RoomsAdminIndeViewModel;
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync()
        {
            return await _context.RoomTypes.ToArrayAsync();
        }



        public void UpdateRoomFeaturesList(Room room, string[] SelectedFeatureIDs)
        {
            var PreviouslySelectedFeatures = _context.RoomFeatureRelationships.Where(x => x.RoomID == room.ID);
            _context.RoomFeatureRelationships.RemoveRange(PreviouslySelectedFeatures);
            _context.SaveChanges();


            if (SelectedFeatureIDs != null)
            {
                foreach (var featureID in SelectedFeatureIDs)
                {
                    var AllFeatureIDs = new HashSet<string>(_context.Features.Select(x => x.ID));
                    if (AllFeatureIDs.Contains(featureID))
                    {
                        _context.RoomFeatureRelationships.Add(new RoomFeature
                        {
                            FeatureID = featureID,
                            RoomID = room.ID
                        });
                    }
                }
                _context.SaveChanges();
            }
        }

        public List<SelectedRoomFeatureViewModel> PopulateSelectedFeaturesForRoom(Room room)
        {
            var viewModel = new List<SelectedRoomFeatureViewModel>();
            var allFeatures = _context.Features;
            if (room.ID == "" || room.ID == null)
            {
                foreach (var feature in allFeatures)
                {
                    viewModel.Add(new SelectedRoomFeatureViewModel
                    {
                        FeatureID = feature.ID,
                        Feature = feature,
                        Selected = false
                    });
                }
            }
            else
            {
                var roomFeatures = _context.RoomFeatureRelationships.Where(x => x.RoomID == room.ID);
                var roomFeatureIDs = new HashSet<string>(roomFeatures.Select(x => x.FeatureID));


                foreach (var feature in allFeatures)
                {
                    viewModel.Add(new SelectedRoomFeatureViewModel
                    {
                        FeatureID = feature.ID,
                        Feature = feature,
                        Selected = roomFeatureIDs.Contains(feature.ID)
                    });
                }
            }

            return viewModel;
        }

        public async Task<AddImagesViewModel> AddImagesAsync(List<IFormFile> files)
        {
            var UploadErrors = new List<string>();
            var AddedImages = new List<Image>();
            var imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            foreach (var formFile in files)
            {

                var _ext = Path.GetExtension(formFile.FileName); //file Extension

                if (formFile.Length > 0 && formFile.Length < 1000000)
                {
                    if (!(_ext == ".jpg" || _ext == ".png" || _ext == ".gif" || _ext == ".jpeg"))
                    {
                        UploadErrors.Add("The File \"" + formFile.FileName + "\" could Not be Uploaded because it has a bad extension --> \"" + _ext + "\"");
                        continue;
                    }

                    string NewFileName;
                    var ExistingFilePath = Path.Combine(imagesFolder, formFile.FileName);
                    var FileNameWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
                    
                    for (var count = 1; File.Exists(ExistingFilePath) == true; count++)
                    {
                        FileNameWithoutExtension = FileNameWithoutExtension + " (" + count.ToString() + ")";
                        
                        var UpdatedFileName = FileNameWithoutExtension + _ext;
                        var UpdatedFilePath = Path.Combine(imagesFolder, UpdatedFileName);
                        ExistingFilePath = UpdatedFilePath;
                        count++;
                    }
                   
                    NewFileName = FileNameWithoutExtension + _ext;
                    var filePath = Path.Combine(imagesFolder, NewFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    var image = new Image
                    {
                        ID = Guid.NewGuid().ToString(),
                        Name = NewFileName,
                        Size = ByteSize.FromBytes(formFile.Length).ToString(),
                        ImageUrl = "~/images/" + NewFileName,
                        FilePath = filePath
                    };
                    AddedImages.Add(image);

                }
                else
                {
                    UploadErrors.Add(formFile.FileName + " Size is not Valid. -->(" + ByteSize.FromBytes(formFile.Length).ToString() + ")... Upload a file less than 1MB");
                }
            }
            _context.Images.AddRange(AddedImages);
            _context.SaveChanges();


            var result = new AddImagesViewModel
            {
                AddedImages = AddedImages,
                UploadErrors = UploadErrors
            };
            return result;
        }

        public async Task RemoveImageAsync(Image image)
        {
            File.Delete(image.FilePath);
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
