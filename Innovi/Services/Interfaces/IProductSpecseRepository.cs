using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IProductSpecseRepository
    {
        Task<ProductSpecseDto> GetByIdAsync(int id);
        Task<ICollection<ProductSpecseDto>> GetAllAsync();
        Task<ICollection<ProductSpecseDto>> GetByProductIdAsync(int ProductId);
        Task<CountListData<ProductSpecseDto>> GetWithPagination(ProductSpecseFilterDto PaginationFiltre);
    }
}
