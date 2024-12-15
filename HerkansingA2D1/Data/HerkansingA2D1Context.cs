using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Models;

namespace HerkansingA2D1.Data
{
    public class HerkansingA2D1Context : DbContext
    {
        public DbSet<HerkansingA2D1.Models.Product> Product { get; set; } = default!;

        public HerkansingA2D1Context(DbContextOptions<HerkansingA2D1Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=HerkansingA2D1;Integrated Security=true;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connection);
        }

        public DbSet<HerkansingA2D1.Models.Product> Products { get; set; } = default!;
        public DbSet<HerkansingA2D1.Models.AppUser> AppUser { get; set; } = default!;

    }
}
