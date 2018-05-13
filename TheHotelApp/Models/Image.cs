using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Models
{
    public class Image
    {
        public Guid ID { get; set; }
        public string ImageUrl { get; set; }
    }
}