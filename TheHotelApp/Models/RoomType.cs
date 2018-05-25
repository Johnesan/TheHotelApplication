using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheHotelApp.Models
{
    public class RoomType
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

    }
}