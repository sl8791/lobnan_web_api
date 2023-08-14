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
        public async Task<CountListData<CountryDto>> GetWithPagination(CountryFilterDto PaginationFiltre)
        {
            var CountryByPage = await DbSet.Countries.Where(p => p.IsDeleted == false).ToListAsync();
            var Countrys = CountryByPage.ToList();
            if (PaginationFiltre.NameEn != null)
            {
                Countrys = Countrys.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Countrys = Countrys.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Countries.CountAsync();
            Countrys = Countrys.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
            if (!entityToFind.IsDeleted)
            {
                var CountryDto = _mapper.Map<CountryDto>(entityToFind);
                return CountryDto;
            }
            return null;
        }

    }
}
