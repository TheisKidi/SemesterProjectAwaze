using AwazeLib.model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace SemesterProjectAwaze.Services
{
    public class HouseOwnerDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseOwner>().HasKey(b => b.OwnerId).HasName(nameof(HouseOwner));

        }

        public virtual DbSet<HouseOwner> HouseOwner { get; set; }
    }
}
