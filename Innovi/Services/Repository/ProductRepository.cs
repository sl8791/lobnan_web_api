using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ProductRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Products
        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            try
            {
                var Product = await DbSet.Products.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Products = Product.ToList();
                List<ProductDto> cats = new List<ProductDto>();
                foreach (var item in Products)
                {
                    ProductDto c = new ProductDto();
                    c = _mapper.Map<ProductDto>(item);
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
        //Filter With Pagination
        public async Task<CountListData<ProductDto>> GetWithPagination(ProductFilterDto PaginationFiltre)
        {
            var ProductByPage = await DbSet.Products.Where(p => p.IsDeleted == false).ToListAsync();
            var Products = ProductByPage.ToList();

            if (PaginationFiltre.ProductCode != null)
            {
                Products = Products.Where(c => c.ProductCode.ToUpper() == PaginationFiltre.ProductCode.ToUpper()).ToList();
            }
            if (PaginationFiltre.Price != null)
            {
                Products = Products.Where(c => c.Price == PaginationFiltre.Price).ToList();
            }
            if (PaginationFiltre.CategoryId != null)
            {
                Products = Products.Where(c => c.CategoryId == PaginationFiltre.CategoryId).ToList();
            }
            if (PaginationFiltre.ProductNameEn != null)
            {
                Products = Products.Where(c => c.ProductNameEn.ToUpper() == PaginationFiltre.ProductNameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.ProductNameAr != null)
            {
                Products = Products.Where(c => c.ProductNameAr.ToUpper() == PaginationFiltre.ProductNameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Products.CountAsync();
            Products = Products.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<ProductDto> cats = new List<ProductDto>();
            foreach (var item in Products)
            {
                cats.Add(_mapper.Map<ProductDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<ProductDto>(cats, totalCount);
            return countListData;
        }
        //Get One Product
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Products.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var productDto = _mapper.Map<ProductDto>(entityToFind);
                return productDto;
            }
            return null;
        }

        //Get Product By CategoryId(fils)
        public async Task<ICollection<ProductDto>> GetByCategoryIdAsync(int CategoryId)
        {
            var productsToFind = await DbSet.Products
                .Where(p => p.CategoryId == CategoryId && !p.IsDeleted).ToListAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(productsToFind);
            return productsDtos;
        }
    }
}
