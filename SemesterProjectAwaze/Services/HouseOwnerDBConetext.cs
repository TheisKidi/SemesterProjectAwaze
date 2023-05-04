using AwazeLib.model;
using Microsoft.EntityFrameworkCore;


namespace SemesterProjectAwaze.Services
{
    public class HouseOwnerDBConetext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        public virtual DbSet<HouseOwner> HouseOwner { get; set; }
    }
}
