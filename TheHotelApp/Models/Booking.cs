using System;
using System.ComponentModel.DataAnnotations.Schema;
using TheHotelApp.Models;

namespace HotelManagementSystem.Models
{
    public class Booking
    {
        public Guid ID { get; set; }
        public Guid RoomID { get; set; }
        public virtual Room Room { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Guests { get; set; }
        public decimal TotalFee { get; set; }
        public bool Paid { get; set; }
        public bool Completed { get; set; }
        public Guid ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationId))]
        public virtual ApplicationUser User { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string OtherRequests { get; set; }
    }
}