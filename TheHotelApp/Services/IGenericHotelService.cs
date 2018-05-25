using TheHotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheHotelApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TheHotelApp.Services
{
    public interface IGenericHotelService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllItemsAsync();

        Task<TEntity> GetItemByIdAsync(string id);

        Task<IEnumerable<TEntity>> SearchFor(Expression<Func<TEntity, bool>> expression);

        Task CreateItemAsync(TEntity entity);

        Task EditItemAsync(TEntity entity);

        Task DeleteItemAsync(TEntity entity);


        //This section contains methods particular to specific controllers
        #region Specific Controller Methods          
       
        RoomsAdminIndexViewModel GetAllRoomsAndRoomTypes();

        Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();

        List<SelectedRoomFeatureViewModel> PopulateSelectedFeaturesForRoom(Room room);

        void UpdateRoomFeaturesList(Room room, string[] SelectedFeatureIDs);



        #endregion


    }
}
