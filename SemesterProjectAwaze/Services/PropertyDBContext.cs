using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class PropertyDBContext : DbContext
    {
        #region property
        public virtual DbSet<Property> Property { get; set; }
        #endregion

        #region methods
        // forklares i FavoriteDbContext
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(b => b.Id).HasName(nameof(Property));
        }
        #endregion
    }
}
