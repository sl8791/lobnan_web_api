using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class CountryRepository: ICountryRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public CountryRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Country
        public async Task<ICollection<CountryDto>> GetAllAsync()
        {
            try
            {
                var Country = await DbSet.Countries.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Countrys = Country.ToList();
                List<CountryDto> Countr = new List<CountryDto>();
                foreach (var item in Countrys)
                {
                    CountryDto c = new CountryDto();
                    c = _mapper.Map<CountryDto>(item);
                    Countr.Add(c);
                }
                if (Countr.Count == 0)
                {
                    return null;
                }
                return Countr;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Filter With Pagination
        public async Task<CountListData<CountryDto>> GetWithPagination(CountryFilterDto PaginationFilter)
        {
            var CountryByPage = await DbSet.Countries.Where(p => p.IsDeleted == false).ToListAsync();
            var Countrys = CountryByPage.ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "NameAr":
                        if (item.Value != null)
                            Countrys = Countrys.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Countrys = Countrys.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }          
            int totalCount = await DbSet.Countries.CountAsync();
            Countrys = Countrys.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<CountryDto> cats = new List<CountryDto>();
            foreach (var item in Countrys)
            {
                cats.Add(_mapper.Map<CountryDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<CountryDto>(cats, totalCount);
            return countListData;
        }
        //Get One Merchant
        public async Task<CountryDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Countries.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var CountryDto = _mapper.Map<CountryDto>(entityToFind);
                return CountryDto;
            }
            return null;
        }

    }
}
