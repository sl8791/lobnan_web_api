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
        public async Task<CountListData<CityDto>> GetWithPagination(CityFilterDto PaginationFiltre)
        {
            var CountryByPage = await DbSet.Cities.Where(p => p.IsDeleted == false).ToListAsync();
            var Countrys = CountryByPage.ToList();
            if (PaginationFiltre.CountryId != null)
            {
                Countrys = Countrys.Where(c => c.CountryId == PaginationFiltre.CountryId).ToList();
            }
            if (PaginationFiltre.NameEn != null)
            {
                Countrys = Countrys.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Countrys = Countrys.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Cities.CountAsync();
            Countrys = Countrys.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<CityDto> cats = new List<CityDto>();
            foreach (var item in Countrys)
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
            if (!entityToFind.IsDeleted)
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
