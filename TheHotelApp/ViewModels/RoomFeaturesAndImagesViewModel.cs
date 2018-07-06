using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotelApp.Models;

namespace TheHotelApp.ViewModels
{
    public class RoomFeaturesAndImagesViewModel
    {
        public List<Image> Images { get; set; }
        public List<Feature> Features { get; set; }
    }
}
