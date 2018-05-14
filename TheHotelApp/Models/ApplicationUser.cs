using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace TheHotelApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
