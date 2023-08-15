using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IAttributeValueRepository
    {
        Task<AttributeValueDto> GetByIdAsync(int id);
        Task<ICollection<AttributeValueDto>> GetAllAsync();
        Task<ICollection<AttributeValueDto>> GetByAttributeIdAsync(int AttributeId);
        Task<CountListData<AttributeValueDto>> GetWithPagination(AttributeValueFilterDto PaginationFiltre);
    }
}
