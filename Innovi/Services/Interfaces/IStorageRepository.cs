using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IStorageRepository
    {
        Task<StorageDto> GetByIdAsync(int id);
        Task<ICollection<StorageDto>> GetAllAsync();
        Task<ICollection<StorageDto>> GetByMerchantIdAsync(int MerchantId);
        Task<CountListData<StorageDto>> GetWithPagination(StorageFilterDto PaginationFiltre);
    }
}
