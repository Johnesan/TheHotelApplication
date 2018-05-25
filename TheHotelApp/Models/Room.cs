using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheHotelApp.Models;

namespace TheHotelApp.Models
{
   

    public class Room
    {
        public string ID { get; set; }
        public int Number { get; set; }
        public string RoomTypeID { get; set; }
        public virtual RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        public int MaximumGuests { get; set; }
        public virtual ICollection<RoomFeature> Features { get; set; }
        public virtual ICollection<Image> RoomImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}