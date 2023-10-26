using Microsoft.EntityFrameworkCore;
using learn.Models.Domain;

namespace learn.Data
{
  public class DefaultDbContext : DbContext
  {
    public DefaultDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
  }
}