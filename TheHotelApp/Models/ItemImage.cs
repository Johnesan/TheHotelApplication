using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheHotelApp.Models
{
    public class ItemImage
    {
        public string ImageID { get; set; }
        public virtual Image Image { get; set; }
        public string ItemID { get; set; }
    }
}
