using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotelApp.Models;

namespace TheHotelApp.ViewModels
{
    public class AddImagesViewModel
    {
        public List<string> UploadErrors { get; set; }
        public List<Image> AddedImages { get; set; }
    }
}
