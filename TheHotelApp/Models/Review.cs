using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheHotelApp.Models
{
    public class Review
    {
        public string ID { get; set; }
        public string RoomID { get; set; }
        public virtual Room Room { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}