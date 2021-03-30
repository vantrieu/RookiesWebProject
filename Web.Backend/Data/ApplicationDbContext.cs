using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Backend.Models;

namespace Web.Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Rate>().HasKey(r => new { r.ProductId, r.UserId });
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<Product>()
                .HasOne<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            //modelBuilder.Entity<Category>()
            //    .HasMany<Product>(c => c.Products)
            //    .WithOne(c => c.Category)
            //    .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Rate>()
                .HasOne<Product>(r => r.Product)
                .WithMany(p => p.Rates)
                .HasForeignKey(r => r.ProductId);
            //modelBuilder.Entity<Product>()
            //    .HasMany<Rate>(p => p.Rates)
            //    .WithOne(p => p.Product)
            //    .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Rate>()
                .HasOne<User>(r => r.User)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserId);
            //modelBuilder.Entity<User>()
            //    .HasMany<Rate>(u => u.Rates)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Order>()
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
            //modelBuilder.Entity<User>()
            //    .HasMany<Order>(u => u.Orders)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne<Order>(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne<Product>(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
