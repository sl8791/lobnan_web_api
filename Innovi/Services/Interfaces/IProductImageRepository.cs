using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IProductImageRepository
    {
        Task<ProductImageDto> GetByIdAsync(int id);
        Task<ICollection<ProductImageDto>> GetAllAsync();
        Task<ICollection<ProductImageDto>> GetByProductIdAsync(int ProductId);
    }
}
