using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class GovernorateRepository: IGovernorateRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public GovernorateRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Governorates
        public async Task<ICollection<GovernorateDto>> GetAllAsync()
        {
            try
            {
                var Governorate = await DbSet.Governorates.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Governorates = Governorate.ToList();
                List<GovernorateDto> Cit = new List<GovernorateDto>();
                foreach (var item in Governorates)
                {
                    GovernorateDto c = new GovernorateDto();
                    c = _mapper.Map<GovernorateDto>(item);
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
        public async Task<CountListData<GovernorateDto>> GetWithPagination(GovernorateFilterDto PaginationFiltre)
        {
            var CountryByPage = await DbSet.Governorates.Where(p => p.IsDeleted == false).ToListAsync();
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

            int totalCount = await DbSet.Governorates.CountAsync();
            Countrys = Countrys.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<GovernorateDto> cats = new List<GovernorateDto>();
            foreach (var item in Countrys)
            {
                cats.Add(_mapper.Map<GovernorateDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<GovernorateDto>(cats, totalCount);
            return countListData;
        }
        //Get One Governorates
        public async Task<GovernorateDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Governorates.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var GovernorateDto = _mapper.Map<GovernorateDto>(entityToFind);
                return GovernorateDto;
            }
            return null;
        }
        //Get Governorates By Country
        public async Task<ICollection<GovernorateDto>> GetByCountryIdAsync(int CountryId)
        {
            var entityToFind = await DbSet.Governorates
                .Where(p => p.CountryId == CountryId && !p.IsDeleted).ToListAsync();
            var GovernorateDto = _mapper.Map<List<GovernorateDto>>(entityToFind);
            return GovernorateDto;
        }
    }
}
