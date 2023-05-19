using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class FavoriteDBContext : DbContext
    {
        #region property
        public virtual DbSet<Favorite> Favorite { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Metoden erstatter den oprindelige OnConfiguring-metode i DbContext-klassen.
        /// Herfter bruger den builder-objektet til at konfigurere DbContext til at bruge 
        /// en SQL Server-database. GetConnectionString er en metode, 
        /// der returnerer forbindelsesstrengen til SQL Server-databasen.
        /// </summary>
        /// <param name="builder">
        /// Modtager en DbContextOptionsBuilder-objekt som parameter, 
        /// som bruges til at konfigurere DbContext.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        /// <summary>
        /// ToTable bruger modelBuilder-objektet til at sætte tabellnavnet for 
        /// "Favorite". Entity<Favorite>() angiver, at det 
        /// skal gælde for "Favorite". ToTable("Favorites") specificerer, 
        /// at tabellen skal have navnet "Favorites" i databasen. Herefter
        /// definerer den primære nøglen fro Favorite.
        /// </summary>
        /// <param name="modelBuilder">
        /// Modtager et ModelBuilder-objekt som parameter, som bruges 
        /// til at definere modelkonfigurationer.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>().ToTable("Favorites");
            modelBuilder.Entity<Favorite>().HasKey(f => f.Id);
        }
        #endregion
    }
}
