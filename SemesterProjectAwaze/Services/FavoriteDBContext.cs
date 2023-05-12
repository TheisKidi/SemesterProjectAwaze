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
            modelBuilder.Entity<Favorite>().ToTable("Favorites");
            modelBuilder.Entity<Favorite>().HasKey(f => f.Id);
        }

        public virtual DbSet<Favorite> Favorite { get; set; }


    }
}
