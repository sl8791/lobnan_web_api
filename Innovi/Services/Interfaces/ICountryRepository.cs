using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ICountryRepository
    {
        Task<CountryDto> GetByIdAsync(int id);
        Task<ICollection<CountryDto>> GetAllAsync();
        Task<CountListData<CountryDto>> GetWithPagination(CountryFilterDto PaginationFiltre);
    }
}
