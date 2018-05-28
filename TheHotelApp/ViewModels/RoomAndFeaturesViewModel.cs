using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotelApp.Models;

namespace TheHotelApp.ViewModels
{
    public class RoomsAndFeaturesViewModel
    {
        public List<Room> Rooms { get; set; }
        public List<RoomFeature> Features { get; set; }
    }
}
