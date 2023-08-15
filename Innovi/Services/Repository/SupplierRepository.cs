using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class SupplierRepository: ISupplierRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public SupplierRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Supplier
        public async Task<ICollection<SupplierDto>> GetAllAsync()
        {
            try
            {
                var supplier = await DbSet.Suppliers.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var suppliers = supplier.ToList();
                List<SupplierDto> cats = new List<SupplierDto>();
                foreach (var item in suppliers)
                {
                    SupplierDto c = new SupplierDto();
                    c = _mapper.Map<SupplierDto>(item);
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
        //Get One Supplier
        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Suppliers.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var supplierDto = _mapper.Map<SupplierDto>(entityToFind);
                return supplierDto;
            }
            return null;
        }
        //Filter With Pagination
        public async Task<CountListData<SupplierDto>> GetWithPagination(SupplierFilterDto PaginationFiltre)
        {
            var SupplierByPage = await DbSet.Suppliers.Where(p => p.IsDeleted == false).ToListAsync();
            var Suppliers = SupplierByPage.ToList();

            if (PaginationFiltre.AddressEn != null)
            {
                Suppliers = Suppliers.Where(c => c.AddressEn.ToUpper() == PaginationFiltre.AddressEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.AddressAr != null)
            {
                Suppliers = Suppliers.Where(c => c.AddressAr.ToUpper() == PaginationFiltre.AddressAr.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameEn != null)
            {
                Suppliers = Suppliers.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Suppliers = Suppliers.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Suppliers.CountAsync();
            Suppliers = Suppliers.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<SupplierDto> cats = new List<SupplierDto>();
            foreach (var item in Suppliers)
            {
                cats.Add(_mapper.Map<SupplierDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<SupplierDto>(cats, totalCount);
            return countListData;
        }
    }
}
