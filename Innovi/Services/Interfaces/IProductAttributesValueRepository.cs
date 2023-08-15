using Innovi.Models.Filters;
using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IProductAttributesValueRepository
    {
        Task<ProductAttributesValueDto> GetByIdAsync(int ProductId, int AttributeValueId, int AttributeId);
        Task<ICollection<ProductAttributesValueDto>> GetAllAsync();
        
    }
}
