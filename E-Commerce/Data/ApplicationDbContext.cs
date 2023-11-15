using E_Commerce.Identity;
using E_Commerce.Models.BannerEntities;
using E_Commerce.Models.CartEntities;
using E_Commerce.Models.CustomerFeedback;
using E_Commerce.Models.OrderEntities;
using E_Commerce.Models.ProductEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Discount)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.PricePerQuantity)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<E_Commerce.Models.OrderEntities.Order> Order { get; set; }
    }
}
