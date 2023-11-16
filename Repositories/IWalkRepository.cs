using learn.Models.Domain;

namespace learn.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync();
    }
}
