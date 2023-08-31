using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class ProductQuantityRepository: IProductQuantityRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ProductQuantityRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All ProductQuantity
        public async Task<ICollection<ProductQuantityDto>> GetAllAsync()
        {
            try
            {
                var ProductQuantity = await DbSet.ProductQuantities.ToListAsync();
                var ProductQuantities = ProductQuantity.ToList();
                List<ProductQuantityDto> cats = new List<ProductQuantityDto>();
                foreach (var item in ProductQuantities)
                {
                    ProductQuantityDto c = new ProductQuantityDto();
                    c = _mapper.Map<ProductQuantityDto>(item);
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

        //Get One ProductQuantity
        public async Task<ProductQuantityDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.ProductQuantities.FindAsync(id);
            if (entityToFind != null && entityToFind != null)
            {
                var ProductQuantityDto = _mapper.Map<ProductQuantityDto>(entityToFind);
                return ProductQuantityDto;
            }
            return null;
        }

        //Get ProductQuantity By ProductId
        public async Task<ICollection<ProductQuantityDto>> GetByProductIdAsync(int ProductId)
        {
            var entityToFind = await DbSet.ProductQuantities.Where(p => p.ProductId == ProductId).ToListAsync();
            var productsDtos = _mapper.Map<List<ProductQuantityDto>>(entityToFind);
            return productsDtos;
        }
    }
}
