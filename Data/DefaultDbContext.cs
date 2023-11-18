using Microsoft.EntityFrameworkCore;
using learn.Models.Domain;

namespace learn.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("cee58370-8dc7-4f3b-b1a3-0a961be18f72"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("721f7b45-2e23-4ba5-a8bc-7b047481fd91"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8324d18e-f798-499d-8fba-a0a80e925412"),
                    Name = "Hard",
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed Data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("44731059-8385-4260-8317-472341e74576"),
                    Code = "GRS",
                    Name = "Gresik",
                    RegionImageUrl = "https://picsum.photos/id/1/400/300"
                },
                new Region
                {
                    Id = Guid.Parse("55f065e4-9959-4d54-b468-f3b8688f0d63"),
                    Code = "KDR",
                    Name = "Kediri",
                    RegionImageUrl = "https://picsum.photos/id/2/400/300"
                },
                new Region
                {
                    Id = Guid.Parse("18c9dfa7-f68a-4344-85d8-aac12c6572cb"),
                    Code = "MDR",
                    Name = "Madura",
                    RegionImageUrl = "https://picsum.photos/id/3/400/300"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
