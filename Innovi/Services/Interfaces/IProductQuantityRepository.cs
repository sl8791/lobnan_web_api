using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IProductQuantityRepository
    {
        Task<ProductQuantityDto> GetByIdAsync(int id);
        Task<ICollection<ProductQuantityDto>> GetAllAsync();
        Task<ICollection<ProductQuantityDto>> GetByProductIdAsync(int ProductId);
    }
}
