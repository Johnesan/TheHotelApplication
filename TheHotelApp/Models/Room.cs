using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TheHotelApp.Models;

namespace TheHotelApp.Models
{
    public class Room
    {
        public string ID { get; set; }
        [Required]
        public int Number { get; set; }

        public string RoomTypeID { get; set; }
        public virtual RoomType RoomType { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool Available { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public int MaximumGuests { get; set; }
        public virtual ICollection<RoomFeature> Features { get; set; }
        public virtual ICollection<Image> RoomImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}