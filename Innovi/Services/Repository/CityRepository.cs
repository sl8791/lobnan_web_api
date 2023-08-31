using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Innovi.Services.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public CityRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All City
        public async Task<ICollection<CityDto>> GetAllAsync()
        {
            try
            {
                var City = await DbSet.Cities.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Citys = City.ToList();
                List<CityDto> Cit = new List<CityDto>();
                foreach (var item in Citys)
                {
                    CityDto c = new CityDto();
                    c = _mapper.Map<CityDto>(item);
                    Cit.Add(c);
                }
                if (Cit.Count == 0)
                {
                    return null;
                }
                return Cit;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Filter With Pagination
        public async Task<CountListData<CityDto>> GetWithPagination(CityFilterDto PaginationFilter)
        {
            var CitieByPage = await DbSet.Cities.Where(p => p.IsDeleted == false).ToListAsync();
            var Cities = CitieByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CountryId", PaginationFilter.CountryId);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "CountryId":
                        if (item.Value != null)
                            Cities = Cities.Where(c => c.CountryId == int.Parse(PaginationFilter.CountryId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Cities = Cities.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Cities = Cities.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }        
            int totalCount = await DbSet.Cities.CountAsync();
            Cities = Cities.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<CityDto> cats = new List<CityDto>();
            foreach (var item in Cities)
            {
                cats.Add(_mapper.Map<CityDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<CityDto>(cats, totalCount);
            return countListData;
        }
        //Get One City
        public async Task<CityDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Cities.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var cityDto = _mapper.Map<CityDto>(entityToFind);
                return cityDto;
            }
            return null;
        }
        //Get City By Country
        public async Task<ICollection<CityDto>> GetByCountryIdAsync(int CountryId)
        {
            var entityToFind = await DbSet.Cities
                .Where(p => p.CountryId == CountryId && !p.IsDeleted).ToListAsync();
            var cityDto = _mapper.Map<List<CityDto>>(entityToFind);
            return cityDto;
        }
    }
}
