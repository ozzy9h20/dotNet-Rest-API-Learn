using learn.Data;
using learn.Models.Domain;

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
    }
}
