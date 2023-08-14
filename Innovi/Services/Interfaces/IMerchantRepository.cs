using Innovi.Models;
using Innovi.Models.Filters;

namespace Innovi.Services.Interfaces
{
    public interface IMerchantRepository  
    {
        Task<MerchantDto> GetByIdAsync(int id);
        Task<ICollection<MerchantDto>> GetAllAsync();
        Task<ICollection<MerchantDto>> GetByCountryIdAsync(int CountryId);
        Task<CountListData<MerchantDto>> GetWithPagination(MerchantFilterDto PaginationFiltre);
    }
}
