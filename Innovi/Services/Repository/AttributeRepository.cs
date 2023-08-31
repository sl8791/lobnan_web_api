using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Innovi.Services.Repository
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public AttributeRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Attribute
        public async Task<ICollection<AttributeDto>> GetAllAsync()
        {
            try
            {
                var attribute = await DbSet.Attributes.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var attributes = attribute.ToList();
                List<AttributeDto> cats = new List<AttributeDto>();
                foreach (var item in attributes)
                {
                    AttributeDto c = new AttributeDto();
                    c = _mapper.Map<AttributeDto>(item);
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
        //Get One Attribute
        public async Task<AttributeDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Attributes.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var AttributeDto = _mapper.Map<AttributeDto>(entityToFind);
                return AttributeDto;
            }
            return null;
        }
        //Get Attribute By MerchantId
        public async Task<ICollection<AttributeDto>> GetByMerchantIdAsync(int merchantId)
        {
            var productsToFind = await DbSet.Attributes
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var AttributeDto = _mapper.Map<List<AttributeDto>>(productsToFind);
            return AttributeDto;
        }
        //Filter With Pagination
        public async Task<CountListData<AttributeDto>> GetWithPagination(AttributeFilterDto PaginationFilter)
        {
            var AttributeByPage = await DbSet.Attributes.Where(p => p.IsDeleted == false).ToListAsync();
            var Attributes = AttributeByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("MerchantId", PaginationFilter.MerchantId);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "MerchantId":
                        if (item.Value != null)
                            Attributes = Attributes.Where(c => c.MerchantId == int.Parse(PaginationFilter.MerchantId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Attributes = Attributes.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Attributes = Attributes.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Attributes.CountAsync();
            Attributes = Attributes.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<AttributeDto> cats = new List<AttributeDto>();
            foreach (var item in Attributes)
            {
                cats.Add(_mapper.Map<AttributeDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<AttributeDto>(cats, totalCount);
            return countListData;
        }
        
    }
}
