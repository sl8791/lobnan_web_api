using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class ProductAttributesValueRepository: IProductAttributesValueRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ProductAttributesValueRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All ProductAttributesValue
        public async Task<ICollection<ProductAttributesValueDto>> GetAllAsync()
        {
            try
            {
                var productAttributesValue = await DbSet.ProductAttributesValues.ToListAsync();
                var productAttributesValues = productAttributesValue.ToList();
                List<ProductAttributesValueDto> cats = new List<ProductAttributesValueDto>();
                foreach (var item in productAttributesValues)
                {
                    ProductAttributesValueDto c = new ProductAttributesValueDto();
                    c = _mapper.Map<ProductAttributesValueDto>(item);
                    cats.Add(c);
                }
                if (cats.Count == 0)
                {
                    return null;
                }
                return cats;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Get One ProductAttributesValue
        public async Task<ProductAttributesValueDto> GetByIdAsync(int productId, int attributeValueId, int attributeId)
        {
            var entityToFind = await DbSet.ProductAttributesValues
                .Where(p => p.ProductId == productId && p.AttributeValueId == attributeValueId && p.AttributeId == attributeId)
                .FirstOrDefaultAsync();
            if (entityToFind != null)
            {
                var productAttributesValueDto = _mapper.Map<ProductAttributesValueDto>(entityToFind);
                return productAttributesValueDto;
            }
            return null;
        }
    }
}
