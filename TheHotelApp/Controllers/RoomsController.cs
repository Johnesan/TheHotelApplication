using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheHotelApp.Models;
using TheHotelApp.Data;
using TheHotelApp.Services;
using TheHotelApp.ViewModels;

namespace TheHotelApp.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IGenericHotelService<Room> _hotelService;

        public RoomsController(IGenericHotelService<Room> genericHotelService)
        {
            _hotelService = genericHotelService;
        }

        // GET: Rooms
        public IActionResult Index()
        {
            return View(_hotelService.GetAllRoomsAndRoomTypes());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _hotelService.GetItemByIdAsync(id);
            var hbv = room.Features;
            

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            var RoomTypes = _hotelService.GetAllRoomTypesAsync().Result;
            ViewData["RoomTypeID"] = new SelectList(RoomTypes, "ID", "Name");

            var room = new Room();
            ViewData["Features"] = _hotelService.PopulateSelectedFeaturesForRoom(room);


            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,RoomTypeID,Price,Available,Description,MaximumGuests")] Room room, string[] SelectedFeatureIDs)
        {
            
            if (ModelState.IsValid)
            {
                room.ID = Guid.NewGuid().ToString();                
                await _hotelService.CreateItemAsync(room);
                _hotelService.UpdateRoomFeaturesList(room, SelectedFeatureIDs);                
                return RedirectToAction(nameof(Index));
            }
            _hotelService.PopulateSelectedFeaturesForRoom(room);
            return View(room);
        }



        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _hotelService.GetItemByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            var RoomTypes = _hotelService.GetAllRoomTypesAsync().Result;
            ViewData["RoomTypeID"] = new SelectList(RoomTypes, "ID", "Name", room.RoomType.ID);


            ViewData["Features"] = _hotelService.PopulateSelectedFeaturesForRoom(room);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Number,RoomTypeID,Price,Available,Description,MaximumGuests")] Room room, string[] SelectedFeatureIDs)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

           
            if (ModelState.IsValid)
            {
                try
                {
                    await _hotelService.EditItemAsync(room);
                    _hotelService.UpdateRoomFeaturesList(room, SelectedFeatureIDs);

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
            var RoomTypes = _hotelService.GetAllRoomTypesAsync().Result;
            ViewData["RoomTypeID"] = new SelectList(RoomTypes, "ID", "ID", room.RoomTypeID);
            _hotelService.PopulateSelectedFeaturesForRoom(room);
            return View(room);
        }




        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _hotelService.GetItemByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var roomType = await _hotelService.GetItemByIdAsync(id);
            await _hotelService.DeleteItemAsync(roomType);
            return RedirectToAction(nameof(Index));
        }


        //This method simply populates a ViewBag with selected features pertaining to the room in question
        //using the SelectedRoomFeatureViewModel which contains a feature and a boolean indicating if the checkbox should be selected or not.
       

  
    }
}
