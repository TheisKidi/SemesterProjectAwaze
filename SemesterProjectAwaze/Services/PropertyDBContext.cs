using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class PropertyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(b => b.Id).HasName(nameof(Property));
            modelBuilder.Entity<Facilities>().HasNoKey();
        }

        public virtual DbSet<Property> Property { get; set; }
    }
}
