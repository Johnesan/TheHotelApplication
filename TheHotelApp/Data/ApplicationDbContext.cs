using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotelApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheHotelApp.Models;

namespace TheHotelApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomFeature> RoomFeatureRelationships { get; set; }
        public DbSet<ItemImage> ItemImageRelationships { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<RoomFeature>()
                .HasKey(x => new { x.RoomID, x.FeatureID });

            builder.Entity<RoomFeature>()
                .HasOne(rf => rf.Room)
                .WithMany(r => r.Features);

            builder.Entity<RoomFeature>()
                .HasOne(f => f.Feature)
                .WithMany(r => r.Rooms);

            builder.Entity<ItemImage>()
                .HasKey(x => new { x.ItemID, x.ImageID });

            builder.Entity<RoomType>()
                .HasMany(b => b.Rooms)
                .WithOne(p => p.RoomType)
    .HasForeignKey(p => p.RoomTypeID)
    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
