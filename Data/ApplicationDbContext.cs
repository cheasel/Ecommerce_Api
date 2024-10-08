using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace eCommerceApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User one to many Relation
            builder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User one to one Relation
            builder.Entity<User>()
                .HasOne(u => u.Vendor)
                .WithOne(v => v.User)
                .HasForeignKey<Vendor>(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(u => u.ShoppingCarts)
                .WithOne(s => s.User)
                .HasForeignKey<ShoppingCart>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Vendor one to many Relation
            builder.Entity<Vendor>()
                .HasMany(v => v.Products)
                .WithOne(p => p.Vendor)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Category one to many Relation
            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order one to one Relation
            builder.Entity<Order>()
                .HasOne(o => o.Payments)
                .WithOne(p  => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem many to many Relation
            builder.Entity<OrderItem>()
                .HasKey(oi => new { 
                    oi.OrderId, oi.ProductId
                });
            
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // CartItem many to many Relation
            builder.Entity<CartItem>()
                .HasKey(ci => new { ci.CartId, ci.ProductId});
            
            builder.Entity<CartItem>()
                .HasOne(ci => ci.ShoppingCart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Review many to many Relation
            builder.Entity<Review>()
                .HasKey(r => r.Id);
            
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // Like many to many Relation
            builder.Entity<Like>()
                .HasKey(l => l.Id);

            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);

            builder.Entity<Like>()
                .HasOne(l => l.Product)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Like>()
                .HasOne(l => l.Review)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.ReviewId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}