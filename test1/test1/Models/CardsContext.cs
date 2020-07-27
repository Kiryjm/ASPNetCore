using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace test1.Models
{
    public class CardsContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public CardsContext(DbContextOptions<CardsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
