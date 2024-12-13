using Microsoft.EntityFrameworkCore;

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

    }
}
