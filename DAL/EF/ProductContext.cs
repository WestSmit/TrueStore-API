using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.EF
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ProductsOfManufacturer> ProductsOfManufacturers { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData( new Product[]
            {
                new Product {Name="FirstProduct", Id=1, Currency = "UAH", Description = "The first test product on this store", IsRemoved=false, Price = 100, Status="Active", Characteristic=""  },
                new Product {Name="SecondProduct", Id=2, Currency = "UAH", Description = "The second test product on this store", IsRemoved=false, Price = 200, Status="Active", Characteristic=""  }
            });
        }
    }
}
