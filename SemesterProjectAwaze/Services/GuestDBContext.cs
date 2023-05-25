using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class GuestDBContext : DbContext
    {
        #region property
        public virtual DbSet<Guest> Guest { get; set; }
        #endregion

        #region methods
        // forklares i FavoriteDbContext
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().HasKey(b => b.MyBookingId);
        }
        #endregion
    }
}
