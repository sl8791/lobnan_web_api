using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class ProductImageRepository: IProductImageRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ProductImageRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All ProductImage
        public async Task<ICollection<ProductImageDto>> GetAllAsync()
        {
            try
            {
                var ProductImage = await DbSet.ProductImages.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var ProductImages = ProductImage.ToList();
                List<ProductImageDto> cats = new List<ProductImageDto>();
                foreach (var item in ProductImages)
                {
                    ProductImageDto c = new ProductImageDto();
                    c = _mapper.Map<ProductImageDto>(item);
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

        //Get One ProductImage
        public async Task<ProductImageDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.ProductImages.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var ProductImageDto = _mapper.Map<ProductImageDto>(entityToFind);
                return ProductImageDto;
            }
            return null;
        }

        //Get ProductImage By ProductId
        public async Task<ICollection<ProductImageDto>> GetByProductIdAsync(int ProductId)
        {
            var entityToFind = await DbSet.ProductImages
                .Where(p => p.ProductId == ProductId && !p.IsDeleted).ToListAsync();
            var productsDtos = _mapper.Map<List<ProductImageDto>>(entityToFind);
            return productsDtos;
        }
        
    }
}
