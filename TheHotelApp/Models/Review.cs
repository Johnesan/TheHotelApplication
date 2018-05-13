using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class Review
    {
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public virtual Room Room { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string Description { get; set; }
    }
}