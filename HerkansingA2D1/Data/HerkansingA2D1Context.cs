﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Models;

namespace HerkansingA2D1.Data
{
    public class HerkansingA2D1Context : IdentityDbContext<AppUser>
    {
        public HerkansingA2D1Context(DbContextOptions<HerkansingA2D1Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=HerkansingA2D1;Integrated Security=true;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity => {
                entity.ToTable("AppUser");
            });
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<AppUser> AppUser { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
    }
}
