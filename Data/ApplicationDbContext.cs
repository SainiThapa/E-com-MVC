using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcomMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcomMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUserLogin<int>>()
        .HasKey(l => new { l.LoginProvider, l.ProviderKey });

        modelBuilder.Entity<IdentityUserRole<int>>()
        .HasKey(r => new { r.UserId, r.RoleId });

        modelBuilder.Entity<IdentityUserToken<int>>()
        .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        modelBuilder.Entity<Item>()
        .HasKey(i => i.Id);

        modelBuilder.Entity<CartItem>()
            .HasKey(c => c.ItemId);

        modelBuilder.Entity<Order>()
        .HasKey(o => o.Id);

        modelBuilder.Entity<Order>()
        .Property(o => o.Id)
        .ValueGeneratedOnAdd(); 
        }
    }
}
