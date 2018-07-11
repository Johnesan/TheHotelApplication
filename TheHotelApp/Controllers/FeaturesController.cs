using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheHotelApp.Data;
using TheHotelApp.Services;
using TheHotelApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace TheHotelApp.Controllers
{
    [Authorize]
    public class FeaturesController : Controller
    {
        private readonly IGenericHotelService<Feature> _hotelService;

        public FeaturesController(IGenericHotelService<Feature> genericHotelService)
        {
            _hotelService = genericHotelService;
        }

        // GET: Features
        public async Task<IActionResult> Index()
        {
            return View(await _hotelService.GetAllItemsAsync());
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _hotelService.GetItemByIdAsync(id);
            

            if (feature == null)
            {
                return NotFound();
            }


            return View(feature);
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Icon")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                feature.ID = Guid.NewGuid().ToString();
                await _hotelService.CreateItemAsync(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _hotelService.GetItemByIdAsync(id);

            var rooms = _hotelService.GetAllRoomsWithFeature(id);
            ViewData["RoomsWithFeature"] = rooms;
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Name,Icon")] Feature feature)
        {
            if (id != feature.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _hotelService.EditItemAsync(feature);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_hotelService.GetItemByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _hotelService.GetItemByIdAsync(id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var feature = await _hotelService.GetItemByIdAsync(id);
            await _hotelService.DeleteItemAsync(feature);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
