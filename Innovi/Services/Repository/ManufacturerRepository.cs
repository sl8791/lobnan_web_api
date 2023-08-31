using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Innovi.Entities;


namespace Innovi.Services.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public ManufacturerRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Manufacturer
        public async Task<ICollection<ManufacturerDto>> GetAllAsync()
        {
            try
            {
                var City = await DbSet.Manufacturers.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Citys = City.ToList();
                List<ManufacturerDto> Cit = new List<ManufacturerDto>();
                foreach (var item in Citys)
                {
                    ManufacturerDto c = new ManufacturerDto();
                    c = _mapper.Map<ManufacturerDto>(item);
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
        public async Task<CountListData<ManufacturerDto>> GetWithPagination(ManufacturerFilterDto PaginationFilter)
        {
            var ManufacturerByPage = await DbSet.Manufacturers.Where(p => p.IsDeleted == false).ToListAsync();
            var Manufacturers = ManufacturerByPage.ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "NameAr":
                        if (item.Value != null)
                            Manufacturers = Manufacturers.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Manufacturers = Manufacturers.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Manufacturers.CountAsync();
            Manufacturers = Manufacturers.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<ManufacturerDto> cats = new List<ManufacturerDto>();
            foreach (var item in Manufacturers)
            {
                cats.Add(_mapper.Map<ManufacturerDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<ManufacturerDto>(cats, totalCount);
            return countListData;
        }
        //Get One Manufacturer
        public async Task<ManufacturerDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Manufacturers.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var manufacturerDto = _mapper.Map<ManufacturerDto>(entityToFind);
                return manufacturerDto;
            }
            return null;
        }
    }
}
