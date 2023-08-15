using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ISupplierRepository
    {
        Task<SupplierDto> GetByIdAsync(int id);
        Task<ICollection<SupplierDto>> GetAllAsync();
        Task<CountListData<SupplierDto>> GetWithPagination(SupplierFilterDto PaginationFiltre);
    }
}
