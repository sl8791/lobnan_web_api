using Innovi.Models;
using Innovi.Models.Filters;

namespace Innovi.Services.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<ManufacturerDto> GetByIdAsync(int id);
        Task<ICollection<ManufacturerDto>> GetAllAsync();
        Task<CountListData<ManufacturerDto>> GetWithPagination(ManufacturerFilterDto PaginationFiltre);
    }
}
