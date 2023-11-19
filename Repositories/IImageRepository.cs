using learn.Models.Domain;

namespace learn.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
