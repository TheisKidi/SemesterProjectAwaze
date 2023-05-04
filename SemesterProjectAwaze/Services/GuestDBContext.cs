using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class GuestDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().HasKey(b => b.MyBookingId).HasName(nameof(Guest));
        }


        public virtual DbSet<Guest> Guest { get; set; }

    }
}
