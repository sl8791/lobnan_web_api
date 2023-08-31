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
        public async Task<CountListData<GovernorateDto>> GetWithPagination(GovernorateFilterDto PaginationFilter)
        {
            var GovernorateByPage = await DbSet.Governorates.Where(p => p.IsDeleted == false).ToListAsync();
            var Governorates = GovernorateByPage.ToList();
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
                            Governorates = Governorates.Where(c => c.CountryId == int.Parse(PaginationFilter.CountryId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Governorates = Governorates.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Governorates = Governorates.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Governorates.CountAsync();
            Governorates = Governorates.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<GovernorateDto> cats = new List<GovernorateDto>();
            foreach (var item in Governorates)
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
            if (entityToFind != null && !entityToFind.IsDeleted)
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
