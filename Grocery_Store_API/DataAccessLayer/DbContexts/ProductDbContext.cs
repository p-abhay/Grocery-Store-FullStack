using DataAccessLayer.EFModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEFModel>()
                .Property(p => p.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProductEFModel>()
                .Property(p => p.Price)
                .HasPrecision(12, 2);
        }

        public DbSet<ProductEFModel> Products { get; set; }
        public DbSet<UserEFModel> Users { get; set; }
        public DbSet<CartEFModel> Carts { get; set; }
        public DbSet<OrderDetailsEFModel> OrderDetails { get; set; }   
        public DbSet<OrderEFModel> Orders { get; set; }
        public DbSet<ReviewEFModel> Reviews { get; set; }

    }
}
