using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


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
        public async Task<CountListData<ManufacturerDto>> GetWithPagination(ManufacturerFilterDto PaginationFiltre)
        {
            var ManufacturerByPage = await DbSet.Manufacturers.Where(p => p.IsDeleted == false).ToListAsync();
            var Manufacturers = ManufacturerByPage.ToList();
            if (PaginationFiltre.NameEn != null)
            {
                Manufacturers = Manufacturers.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Manufacturers = Manufacturers.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Manufacturers.CountAsync();
            Manufacturers = Manufacturers.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
            if (!entityToFind.IsDeleted)
            {
                var manufacturerDto = _mapper.Map<ManufacturerDto>(entityToFind);
                return manufacturerDto;
            }
            return null;
        }
    }
}
