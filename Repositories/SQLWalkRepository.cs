using learn.Data;
using learn.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace learn.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly DefaultDbContext dbContext;

        public SQLWalkRepository(DefaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null
        )
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (
                string.IsNullOrWhiteSpace(filterOn) == false
                && string.IsNullOrWhiteSpace(filterQuery) == false
            )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await GetByIdAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await GetByIdAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
