using learn.Data;
using learn.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace learn.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DefaultDbContext dbContext;

        public SQLRegionRepository(DefaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllAsync(string? filterOn, string? filterQuery)
        {
            var regions = dbContext.Regions.AsQueryable();

            if (
                string.IsNullOrWhiteSpace(filterOn) == false
                && string.IsNullOrWhiteSpace(filterQuery) == false
            )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = regions.Where(x => x.Name.Contains(filterQuery));
                }
            }

            return await regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FindAsync(id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exisitingRegion = await GetByIdAsync(id);

            if (exisitingRegion == null)
            {
                return null;
            }

            exisitingRegion.Code = region.Code;
            exisitingRegion.Name = region.Name;
            exisitingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return exisitingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await GetByIdAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
