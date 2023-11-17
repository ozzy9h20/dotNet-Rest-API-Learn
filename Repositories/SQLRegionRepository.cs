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

        public async Task<List<Region>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
        )
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

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending
                        ? regions.OrderBy(x => x.Name)
                        : regions.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending
                        ? regions.OrderBy(x => x.Code)
                        : regions.OrderByDescending(x => x.Code);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;
            return await regions.Skip(skipResults).Take(pageSize).ToListAsync();
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
