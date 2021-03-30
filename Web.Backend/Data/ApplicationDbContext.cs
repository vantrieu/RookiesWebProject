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

            modelBuilder.Entity<Product>()
                .HasOne<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Rate>()
                .HasOne<Product>(r => r.Product)
                .WithMany(p => p.Rates)
                .HasForeignKey(r => r.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany<Rate>(p => p.Rates)
                .WithOne(p => p.Product)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Rate>()
                .HasOne<User>(r => r.User)
                .WithMany(p => p.Rates)
                .HasForeignKey(r => r.UserId);
            modelBuilder.Entity<User>()
                .HasMany<Rate>(u => u.Rates)
                .WithOne(u => u.User)
                .HasForeignKey(r => r.UserId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
