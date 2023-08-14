using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IGovernorateRepository
    {
        Task<GovernorateDto> GetByIdAsync(int id);
        Task<ICollection<GovernorateDto>> GetAllAsync();
        Task<CountListData<GovernorateDto>> GetWithPagination(GovernorateFilterDto PaginationFiltre);
        Task<ICollection<GovernorateDto>> GetByCountryIdAsync(int CountryId);
    }
}
