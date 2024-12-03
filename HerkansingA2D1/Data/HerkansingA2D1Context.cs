using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HerkansingA2D1.Models;

namespace HerkansingA2D1.Data
{
    public class HerkansingA2D1Context : DbContext
    {
        public HerkansingA2D1Context (DbContextOptions<HerkansingA2D1Context> options)
            : base(options)
        {
        }

        public DbSet<HerkansingA2D1.Models.Product> Product { get; set; } = default!;
    }
}
