using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public MerchantRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Merchant
        public async Task<ICollection<MerchantDto>> GetAllAsync()
        {
            try
            {
                var Merchant = await DbSet.Merchants.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Merchants = Merchant.ToList();
                List<MerchantDto> Merch = new List<MerchantDto>();
                foreach (var item in Merchants)
                {
                    MerchantDto c = new MerchantDto();
                    c = _mapper.Map<MerchantDto>(item);
                    Merch.Add(c);
                }
                if (Merch.Count == 0)
                {
                    return null;
                }
                return Merch;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Filter With Pagination
        public async Task<CountListData<MerchantDto>> GetWithPagination(MerchantFilterDto PaginationFiltre)
        {
            var MerchantByPage = await DbSet.Merchants.Where(p => p.IsDeleted == false).ToListAsync();
            var Merchants = MerchantByPage.ToList();

            if (PaginationFiltre.Email != null)
            {
                Merchants = Merchants.Where(c => c.Email.ToUpper() == PaginationFiltre.Email.ToUpper()).ToList();
            }
            if (PaginationFiltre.CountryId != null)
            {
                Merchants = Merchants.Where(c => c.CountryId == PaginationFiltre.CountryId).ToList();
            }
            if (PaginationFiltre.NameEn != null)
            {
                Merchants = Merchants.Where(c => c.NameEn.ToUpper() == PaginationFiltre.NameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.NameAr != null)
            {
                Merchants = Merchants.Where(c => c.NameAr.ToUpper() == PaginationFiltre.NameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Merchants.CountAsync();
            Merchants = Merchants.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<MerchantDto> cats = new List<MerchantDto>();
            foreach (var item in Merchants)
            {
                cats.Add(_mapper.Map<MerchantDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<MerchantDto>(cats, totalCount);
            return countListData;
        }
        //Get One Merchant
        public async Task<MerchantDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Merchants.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var MerchantDto = _mapper.Map<MerchantDto>(entityToFind);
                return MerchantDto;
            }
            return null;
        }

        //Get Merchant By CountryId
        public async Task<ICollection<MerchantDto>> GetByCountryIdAsync(int CountryId)
        {
            var entityToFind = await DbSet.Merchants
                .Where(p => p.CountryId == CountryId && !p.IsDeleted).ToListAsync();
            var productsDtos = _mapper.Map<List<MerchantDto>>(entityToFind);
            return productsDtos;
        }
        
    }
}
