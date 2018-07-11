using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheHotelApp.Data;
using TheHotelApp.Models;
using TheHotelApp.Services;

namespace TheHotelApp.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {
        private readonly IGenericHotelService<Image> _hotelService;

        public ImagesController(IGenericHotelService<Image> genericHotelService)
        {
            _hotelService = genericHotelService;
        }


        // GET: Images
        public async Task<IActionResult> Index()
        {
            return View(await _hotelService.GetAllItemsAsync());
        }

        // GET: Images
        public async Task<IActionResult> GetAllImagesJson()
        {
            var images = await _hotelService.GetAllItemsAsync();            
            return PartialView("GetAllImagesPartial", images);
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _hotelService.GetItemByIdAsync(id);

            if (image == null)
            {
                return NotFound();
            }


            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> files)
        {
            var result = await _hotelService.AddImagesAsync(files);
            var AddedImages = new List<string>();
            foreach(var image in result.AddedImages)
            {
                AddedImages.Add(image.Name + " Added Successfully");
            }
            ViewData["AddedImages"] = AddedImages;
            ViewData["UploadErrors"] = result.UploadErrors;
            return View();
        }

        //// GET: Images/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images.SingleOrDefaultAsync(m => m.ID == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(image);
        //}

        //// POST: Images/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("ID,MyProperty,ImageUrl")] Image image)
        //{
        //    if (id != image.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(image);
        //}

        // GET: Images/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _hotelService.GetItemByIdAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var image = await _hotelService.GetItemByIdAsync(id);
            await _hotelService.RemoveImageAsync(image);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
