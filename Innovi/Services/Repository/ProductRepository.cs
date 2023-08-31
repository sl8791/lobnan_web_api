using AutoMapper;
using Innovi.Data;
using Innovi.Entities;
using Innovi.Models;
using Innovi.Models.Filters;
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
        public async Task<CountListData<ProductDto>> GetWithPagination(ProductFilterDto PaginationFilter)
        {
            var ProductByPage = await DbSet.Products.Where(p => p.IsDeleted == false).ToListAsync();
            var Products = ProductByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ProductCode", PaginationFilter.ProductCode);
            dic.Add("Price", PaginationFilter.Price);
            dic.Add("CategoryId", PaginationFilter.CategoryId);
            dic.Add("ProductNameEn", PaginationFilter.ProductNameEn);
            dic.Add("ProductNameAr", PaginationFilter.ProductNameAr);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "ProductCode":
                        if (item.Value != null)
                            Products = Products.Where(c => c.ProductCode.ToUpper() == PaginationFilter.ProductCode.ToUpper()).ToList();
                        break;
                    case "Price":
                        if (item.Value != null)
                            Products = Products.Where(c => c.Price == int.Parse(PaginationFilter.Price)).ToList();
                        break;
                    case "CategoryId":
                        if (item.Value != null)
                            Products = Products.Where(c => c.CategoryId == int.Parse(PaginationFilter.CategoryId)).ToList();
                        break;
                    case "ProductNameEn":
                        if (item.Value != null)
                            Products = Products.Where(c => c.ProductNameEn.ToUpper() == PaginationFilter.ProductNameEn.ToUpper()).ToList();
                        break;
                    case "ProductNameAr":
                        if (item.Value != null)
                            Products = Products.Where(c => c.ProductNameAr.ToUpper() == PaginationFilter.ProductNameAr.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }         
            int totalCount = await DbSet.Products.CountAsync();
            Products = Products.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
            if (entityToFind != null && !entityToFind.IsDeleted)
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
        //Get Product By MerchantId
        public async Task<ICollection<ProductDto>> GetByMerchantIdAsync(int merchantId)
        {
            var productsToFind = await DbSet.Products
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var productDto = _mapper.Map<List<ProductDto>>(productsToFind);
            return productDto;
        }
        //Get Product By ManufacturerId
        public async Task<ICollection<ProductDto>> GetByManufacturerIdAsync(int manufacturerId)
        {
            var productsToFind = await DbSet.Products
                .Where(p => p.ManufacturerId == manufacturerId && !p.IsDeleted).ToListAsync();
            var productDto = _mapper.Map<List<ProductDto>>(productsToFind);
            return productDto;
        }
    }
}
