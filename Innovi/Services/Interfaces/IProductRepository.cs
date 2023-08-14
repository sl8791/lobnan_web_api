using Innovi.Models;
using Innovi.Models.Filters;

namespace Innovi.Services.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<ICollection<ProductDto>> GetAllAsync();
        Task<ICollection<ProductDto>> GetByCategoryIdAsync(int CategoryId);
        Task<ICollection<ProductDto>> GetByMerchantIdAsync(int MerchantId);
        Task<CountListData<ProductDto>> GetWithPagination(ProductFilterDto PaginationFiltre);
    }
}
