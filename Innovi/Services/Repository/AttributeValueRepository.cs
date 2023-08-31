using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class AttributeValueRepository: IAttributeValueRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public AttributeValueRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All AttributeValue
        public async Task<ICollection<AttributeValueDto>> GetAllAsync()
        {
            try
            {
                var AttributeValue = await DbSet.AttributeValues.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var AttributeValues = AttributeValue.ToList();
                List<AttributeValueDto> cats = new List<AttributeValueDto>();
                foreach (var item in AttributeValues)
                {
                    AttributeValueDto c = new AttributeValueDto();
                    c = _mapper.Map<AttributeValueDto>(item);
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
        //Get One AttributeValue
        public async Task<AttributeValueDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.AttributeValues.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var AttributeValueDto = _mapper.Map<AttributeValueDto>(entityToFind);
                return AttributeValueDto;
            }
            return null;
        }

        //Get AttributeValue By CategoryId
        public async Task<ICollection<AttributeValueDto>> GetByAttributeIdAsync(int AttributeId)
        {
            var productsToFind = await DbSet.AttributeValues
                .Where(p => p.AttributeId == AttributeId && !p.IsDeleted).ToListAsync();
            var productsDtos = _mapper.Map<List<AttributeValueDto>>(productsToFind);
            return productsDtos;
        }
        //Filter With Pagination
        public async Task<CountListData<AttributeValueDto>> GetWithPagination(AttributeValueFilterDto PaginationFilter)
        {
            var ProductByPage = await DbSet.AttributeValues.Where(p => p.IsDeleted == false).ToListAsync();
            var Products = ProductByPage.ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("AttributeId", PaginationFilter.AttributeId);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "Code":
                        if (item.Value != null)
                            Products = Products.Where(c => c.AttributeId == int.Parse(PaginationFilter.AttributeId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Products = Products.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Products = Products.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.AttributeValues.CountAsync();
            Products = Products.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<AttributeValueDto> cats = new List<AttributeValueDto>();
            foreach (var item in Products)
            {
                cats.Add(_mapper.Map<AttributeValueDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<AttributeValueDto>(cats, totalCount);
            return countListData;
        }

    }
}
