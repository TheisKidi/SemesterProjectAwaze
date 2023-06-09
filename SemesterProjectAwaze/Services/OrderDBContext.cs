﻿using AwazeLib.model;
using Microsoft.EntityFrameworkCore;

namespace SemesterProjectAwaze.Services
{
    public class OrderDBContext : DbContext
    {
        #region property
        public virtual DbSet<Order> Order { get; set; }
        #endregion

        #region methods
        // forklares i FavoriteDbContext
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Secret.GetConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasKey(f => f.OrderId);
        }
        #endregion
    }
}
