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
        public async Task<CountListData<MerchantDto>> GetWithPagination(MerchantFilterDto PaginationFilter)
        {
            var MerchantByPage = await DbSet.Merchants.Where(p => p.IsDeleted == false).ToListAsync();
            var Merchants = MerchantByPage.ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CountryId", PaginationFilter.CountryId);
            dic.Add("Email", PaginationFilter.Email);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "CountryId":
                        if (item.Value != null)
                            Merchants = Merchants.Where(c => c.CountryId == int.Parse(PaginationFilter.CountryId)).ToList();
                        break;
                    case "Email":
                        if (item.Value != null)
                            Merchants = Merchants.Where(c => c.Email.ToUpper() == PaginationFilter.Email.ToUpper()).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Merchants = Merchants.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Merchants = Merchants.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Merchants.CountAsync();
            Merchants = Merchants.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
            if (entityToFind != null && !entityToFind.IsDeleted)
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
