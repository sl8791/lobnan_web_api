using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ICityRepository
    {
        Task<CityDto> GetByIdAsync(int id);
        Task<ICollection<CityDto>> GetAllAsync();
        Task<CountListData<CityDto>> GetWithPagination(CityFilterDto PaginationFiltre);
        Task<ICollection<CityDto>> GetByCountryIdAsync(int CountryId);
    }
}
