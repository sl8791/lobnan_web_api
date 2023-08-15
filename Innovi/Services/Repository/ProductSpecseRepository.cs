using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class ProductSpecseRepository: IProductSpecseRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ProductSpecseRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All ProductSpecse
        public async Task<ICollection<ProductSpecseDto>> GetAllAsync()
        {
            try
            {
                var Product = await DbSet.ProductSpecses.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Products = Product.ToList();
                List<ProductSpecseDto> cats = new List<ProductSpecseDto>();
                foreach (var item in Products)
                {
                    ProductSpecseDto c = new ProductSpecseDto();
                    c = _mapper.Map<ProductSpecseDto>(item);
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
        public async Task<CountListData<ProductSpecseDto>> GetWithPagination(ProductSpecseFilterDto PaginationFiltre)
        {
            var ProductByPage = await DbSet.ProductSpecses.Where(p => p.IsDeleted == false).ToListAsync();
            var Products = ProductByPage.ToList();

            if (PaginationFiltre.ProductId != null)
            {
                Products = Products.Where(c => c.ProductId == PaginationFiltre.ProductId).ToList();
            }
            if (PaginationFiltre.ValueAr != null)
            {
                Products = Products.Where(c => c.ValueAr.ToUpper() == PaginationFiltre.ValueAr.ToUpper()).ToList();
            }
            if (PaginationFiltre.ValueEn != null)
            {
                Products = Products.Where(c => c.ValueEn.ToUpper() == PaginationFiltre.ValueEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameEn != null)
            {
                Products = Products.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Products = Products.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.ProductSpecses.CountAsync();
            Products = Products.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<ProductSpecseDto> cats = new List<ProductSpecseDto>();
            foreach (var item in Products)
            {
                cats.Add(_mapper.Map<ProductSpecseDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<ProductSpecseDto>(cats, totalCount);
            return countListData;
        }
        //Get One ProductSpecse
        public async Task<ProductSpecseDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.ProductSpecses.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var ProductSpecseDto = _mapper.Map<ProductSpecseDto>(entityToFind);
                return ProductSpecseDto;
            }
            return null;
        }

        //Get ProductSpecse By ProductId
        public async Task<ICollection<ProductSpecseDto>> GetByProductIdAsync(int CategoryId)
        {
            var productsToFind = await DbSet.ProductSpecses
                .Where(p => p.ProductId == CategoryId && !p.IsDeleted).ToListAsync();
            var productsDtos = _mapper.Map<List<ProductSpecseDto>>(productsToFind);
            return productsDtos;
        }
    }
}
