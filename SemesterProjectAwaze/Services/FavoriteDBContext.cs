using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class FavoriteDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>().HasKey(f => f.Id).HasName(nameof(Favorite));
           
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User).WithMany().
                HasForeignKey(f => f.User.MyBookingId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Property).WithMany().
                HasForeignKey(f => f.Property.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public virtual DbSet<Favorite> Favorite { get; set; }


    }
}
