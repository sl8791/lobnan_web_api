using Innovi.Models.Filters;
using Innovi.Models;
using System.Threading.Tasks;

namespace Innovi.Services.Interfaces
{
    public interface IAttributeRepository
    {
        Task<AttributeDto> GetByIdAsync(int id);
        Task<ICollection<AttributeDto>> GetAllAsync();
        Task<ICollection<AttributeDto>> GetByMerchantIdAsync(int MerchantId);
        Task<CountListData<AttributeDto>> GetWithPagination(AttributeFilterDto PaginationFiltre);
    }
}
