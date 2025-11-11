using Microsoft.EntityFrameworkCore;
using Project_Skynetz.Models;

namespace Project_Skynetz.Data
{
    public class SkynetzContext : DbContext
    {
        public SkynetzContext(DbContextOptions<SkynetzContext> options) : base(options)
        {
        }
        
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Plan> Plans { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Rate>().HasData(
                new Rate { Id = 1, OriginCode = "011", DestinationCode = "016", PricePerMinute = 1.90m },
                new Rate { Id = 2, OriginCode = "016", DestinationCode = "011", PricePerMinute = 2.90m },
                new Rate { Id = 3, OriginCode = "011", DestinationCode = "017", PricePerMinute = 1.70m },
                new Rate { Id = 4, OriginCode = "017", DestinationCode = "011", PricePerMinute = 2.70m },
                new Rate { Id = 5, OriginCode = "011", DestinationCode = "018", PricePerMinute = 0.90m },
                new Rate { Id = 6, OriginCode = "018", DestinationCode = "011", PricePerMinute = 1.90m }
            );
            
            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, Name = "FaleMais 30", IncludedMinutes = 30 },
                new Plan { Id = 2, Name = "FaleMais 60", IncludedMinutes = 60 },
                new Plan { Id = 3, Name = "FaleMais 120", IncludedMinutes = 120 }
            );
        }
    }
}
