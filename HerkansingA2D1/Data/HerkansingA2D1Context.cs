using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Models;

namespace HerkansingA2D1.Data
{
    public class HerkansingA2D1Context : DbContext
    {
        public DbSet<HerkansingA2D1.Models.Product> Product { get; set; } = default!;
        public DbSet<HerkansingA2D1.Models.Product> Products { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important if using Identity

            // Configure Order-User relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User) // An Order has one User
                .WithMany(u => u.Orders) // A User can have many Orders
                .HasForeignKey(o => o.UserId) // Foreign key in Order table
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if User is removed
        }

        public HerkansingA2D1Context(DbContextOptions<HerkansingA2D1Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=HerkansingA2D1;Integrated Security=true;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connection);
        }

    }
}
