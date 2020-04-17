using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.EF
{
    public class ProductContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductByBrand> ProductsByBrands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<ProductByCategory> ProductsByCategories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<IdentityUserClaim<string>> IdentityUserClaim { get; set; }
        public new DbSet<User> Users { get; set; }
        public new DbSet<Role> Roles { get; set; }      

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            this.Database.EnsureCreated();
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Role>().HasData(new Role("User"), new Role("Admin"));

            builder.Entity<IdentityUser>().ToTable("Users","dbo");
            builder.Entity<IdentityRole>().ToTable("Roles","dbo");
        }
    }
}
