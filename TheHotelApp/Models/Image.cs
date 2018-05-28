using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheHotelApp.Models
{
    public class Image
    {
        public string ID { get; set; }
        public string Name { get; set; }        
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string FilePath { get; set; }
    }
}