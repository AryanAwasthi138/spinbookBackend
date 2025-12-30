using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TableTennisBooking.Models;

namespace TableTennisBooking.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Slot>()
                .HasIndex(s => new { s.TableId, s.StartTime, s.EndTime })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}




