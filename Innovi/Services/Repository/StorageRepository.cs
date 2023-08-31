using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class StorageRepository: IStorageRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public StorageRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Storages
        public async Task<ICollection<StorageDto>> GetAllAsync()
        {
            try
            {
                var Product = await DbSet.Storages.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Products = Product.ToList();
                List<StorageDto> cats = new List<StorageDto>();
                foreach (var item in Products)
                {
                    StorageDto c = new StorageDto();
                    c = _mapper.Map<StorageDto>(item);
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
        //Get One Storage
        public async Task<StorageDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Storages.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var storageDto = _mapper.Map<StorageDto>(entityToFind);
                return storageDto;
            }
            return null;
        }

        //Get Storages By MerchantId
        public async Task<ICollection<StorageDto>> GetByMerchantIdAsync(int merchantId)
        {
            var entityToFind = await DbSet.Storages
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var storageDto = _mapper.Map<List<StorageDto>>(entityToFind);
            return storageDto;
        }
        //Filter With Pagination
        public async Task<CountListData<StorageDto>> GetWithPagination(StorageFilterDto PaginationFilter)
        {
            var StoragesByPage = await DbSet.Storages.Where(p => p.IsDeleted == false).ToListAsync();
            var Storages = StoragesByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("MerchantId", PaginationFilter.MerchantId);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "CountryId":
                        if (item.Value != null)
                            Storages = Storages.Where(c => c.MerchantId == int.Parse(PaginationFilter.MerchantId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Storages = Storages.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Storages = Storages.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Storages.CountAsync();
            Storages = Storages.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<StorageDto> cats = new List<StorageDto>();
            foreach (var item in Storages)
            {
                cats.Add(_mapper.Map<StorageDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<StorageDto>(cats, totalCount);
            return countListData;
        }
    }
}
