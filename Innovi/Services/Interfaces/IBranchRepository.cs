using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IBranchRepository
    {
        Task<BranchDto> GetByIdAsync(int id);
        Task<ICollection<BranchDto>> GetAllAsync();
        Task<ICollection<BranchDto>> GetByCityIdAsync(int CityId);
        Task<ICollection<BranchDto>> GetByMerchantIdAsync(int MerchantId);
        Task<CountListData<BranchDto>> GetWithPagination(BranchFilterDto PaginationFiltre);
    }
}
