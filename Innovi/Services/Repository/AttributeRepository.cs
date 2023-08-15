using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            if (!entityToFind.IsDeleted)
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
        public async Task<CountListData<AttributeDto>> GetWithPagination(AttributeFilterDto PaginationFiltre)
        {
            var AttributeByPage = await DbSet.Attributes.Where(p => p.IsDeleted == false).ToListAsync();
            var Attributes = AttributeByPage.ToList();

            if (PaginationFiltre.MerchantId != null)
            {
                Attributes = Attributes.Where(c => c.MerchantId == PaginationFiltre.MerchantId).ToList();
            }
            if (PaginationFiltre.NameEn != null)
            {
                Attributes = Attributes.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Attributes = Attributes.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Attributes.CountAsync();
            Attributes = Attributes.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
