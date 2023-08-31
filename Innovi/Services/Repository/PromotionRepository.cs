using AutoMapper;
using Innovi.Data;
using Innovi.Entities;
using Innovi.Models;
using Innovi.Models.Filters;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public PromotionRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Promotions
        public async Task<ICollection<PromotionDto>> GetAllAsync()
        {
            try
            {
                var Promotion = await DbSet.Promotions.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var Promotions = Promotion.ToList();
                List<PromotionDto> Proms = new List<PromotionDto>();
                foreach (var item in Promotions)
                {
                    PromotionDto c = new PromotionDto();
                    c = _mapper.Map<PromotionDto>(item);
                    Proms.Add(c);
                }
                if (Proms.Count == 0)
                {
                    return null;
                }
                return Proms;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Filter With Pagination
        public async Task<CountListData<PromotionDto>> GetWithPagination(PromotionFilterDto PaginationFilter)
        {
            var PromoByPage = await DbSet.Promotions.Where(p => p.IsDeleted == false).ToListAsync();
            var Promotions = PromoByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("PromotionCode", PaginationFilter.PromotionCode);
            dic.Add("DiscountType", PaginationFilter.DiscountType);
            dic.Add("CategoryId", PaginationFilter.CategoryId);
            dic.Add("PromotionNameEn", PaginationFilter.PromotionNameEn);
            dic.Add("PromotionNameAr", PaginationFilter.PromotionNameAr);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "PromotionCode":
                        if (item.Value != null)
                            Promotions = Promotions.Where(c => c.PromotionCode.ToUpper() == PaginationFilter.PromotionCode.ToUpper()).ToList();
                        break;
                    case "DiscountType":
                        if (item.Value != null)
                            Promotions = Promotions.Where(c => c.DiscountType == int.Parse(PaginationFilter.DiscountType)).ToList();
                        break;
                    case "CategoryId":
                        if (item.Value != null)
                            Promotions = Promotions.Where(c => c.CategoryId == int.Parse(PaginationFilter.CategoryId)).ToList();
                        break;
                    case "PromotionNameEn":
                        if (item.Value != null)
                            Promotions = Promotions.Where(c => c.PromotionNameEn.ToUpper() == PaginationFilter.PromotionNameEn.ToUpper()).ToList();
                        break;
                    case "PromotionNameAr":
                        if (item.Value != null)
                            Promotions = Promotions.Where(c => c.PromotionNameAr.ToUpper() == PaginationFilter.PromotionNameAr.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
            int totalCount = await DbSet.Promotions.CountAsync();
            Promotions = Promotions.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<PromotionDto> Proms = new List<PromotionDto>();
            foreach (var item in Promotions)
            {
                Proms.Add(_mapper.Map<PromotionDto>(item));
            }
            if (Proms.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<PromotionDto>(Proms, totalCount);
            return countListData;
        }
        //Get One Promotion
        public async Task<PromotionDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Promotions.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var promotionDto = _mapper.Map<PromotionDto>(entityToFind);
                return promotionDto;
            }
            return null;
        }

        //Get Promotion By CategoryId(fils)
        public async Task<ICollection<PromotionDto>> GetByCategoryIdAsync(int CategoryId)
        {
            var productsToFind = await DbSet.Promotions
                .Where(p => p.CategoryId == CategoryId && !p.IsDeleted).ToListAsync();
            var promotionDto = _mapper.Map<List<PromotionDto>>(productsToFind);
            return promotionDto;
        }

        //Get Promotion By MerchantId
        public async Task<ICollection<PromotionDto>> GetByMerchantIdAsync(int merchantId)
        {
            var promotionsToFind = await DbSet.Promotions
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var promotionDto = _mapper.Map<List<PromotionDto>>(promotionsToFind);
            return promotionDto;
        }
    }
}
