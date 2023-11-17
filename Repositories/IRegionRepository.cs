using learn.Models.Domain;

namespace learn.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateAsync(Region region);

        Task<List<Region>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true
        );

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);
    }
}
