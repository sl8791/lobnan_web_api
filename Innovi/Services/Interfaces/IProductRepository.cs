using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<ICollection<ProductDto>> GetAllAsync();
        Task<ICollection<ProductDto>> GetByCategoryIdAsync(int CategoryId);
        Task<CountListData<ProductDto>> GetWithPagination(ProductFilterDto PaginationFiltre);
    }
}
