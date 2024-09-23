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

            //builder.Entity<User>().Property(u => u.Id).HasColumnType("int");

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
            
            builder.Entity<User>()
                .HasMany(u => u.ShoppingCarts)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User one to one Relation
            builder.Entity<User>()
                .HasOne(u => u.Vendor)
                .WithOne(v => v.User)
                .HasForeignKey<Vendor>(v => v.UserId)
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
                .OnDelete(DeleteBehavior.Cascade);

            // Order one to many Relation
            builder.Entity<Order>()
                .HasMany(o => o.Payments)
                .WithOne(p  => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem many to many Relation
            builder.Entity<OrderItem>()
                .HasKey(o => new { o.OrderId, o.ProductId});
            
            builder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem many to many Relation
            builder.Entity<CartItem>()
                .HasKey(c => new { c.CartId, c.ProductId});
            
            builder.Entity<CartItem>()
                .HasOne(c => c.ShoppingCart)
                .WithMany(s => s.CartItems)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review many to many Relation
            builder.Entity<Review>()
                .HasKey(r => new { r.UserId, r.ProductId});
            
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Like many to many Relation
            builder.Entity<Like>()
                .HasKey(l => new { l.UserId, l.ProductId, l.ReviewId});

            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId);

            builder.Entity<Like>()
                .HasOne(l => l.Product)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(l => l.Review)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);
            /*builder.Entity<Like>()
                .HasKey(l => l.Id);
            
            builder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Like>()
                .HasOne(l => l.Product)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ProductId).OnDelete(DeleteBehavior.Restrict);*/

            /*builder.Entity<Like>()
                .HasOne(l => l.Review)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => l.ReviewId).OnDelete(DeleteBehavior.Restrict);*/

            builder.Entity<Like>()
                .HasOne(l => l.Review)
                .WithMany(r => r.Likes)
                .HasForeignKey(l => new { l.UserId, l.ProductId }).OnDelete(DeleteBehavior.Restrict);

            /*List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole{
                    Name = "Vendor",
                    NormalizedName = "VENDOR"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);*/
        }
    }
}