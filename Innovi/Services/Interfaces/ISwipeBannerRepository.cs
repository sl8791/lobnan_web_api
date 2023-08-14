using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ISwipeBannerRepository
    {
        Task<SwipeBannerDto> GetByIdAsync(int id);
        Task<ICollection<SwipeBannerDto>> GetAllAsync();
        Task<ICollection<SwipeBannerDto>> GetByCategoryIdAsync(int CategoryId);
    }
}
