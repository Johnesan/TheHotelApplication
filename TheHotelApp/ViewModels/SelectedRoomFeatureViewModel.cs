using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotelApp.Models;

namespace TheHotelApp.ViewModels
{
    public class SelectedRoomFeatureViewModel
    {
        public string FeatureID { get; set; }
        public virtual Feature Feature { get; set; }
        public bool Selected { get; set; }
    }
}
