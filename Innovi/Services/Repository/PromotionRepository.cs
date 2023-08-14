using AutoMapper;
using Innovi.Data;
using Innovi.Models;
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
        //Get All Category
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
        public async Task<CountListData<PromotionDto>> GetWithPagination(PromotionFilterDto PaginationFiltre)
        {
            var PromoByPage = await DbSet.Promotions.Where(p => p.IsDeleted == false).ToListAsync();
            var Promotions = PromoByPage.ToList();

            if (PaginationFiltre.PromotionCode != null)
            {
                Promotions = Promotions.Where(c => c.PromotionCode.ToUpper() == PaginationFiltre.PromotionCode.ToUpper()).ToList();
            }
            if (PaginationFiltre.DiscountType != null)
            {
                Promotions = Promotions.Where(c => c.DiscountType == PaginationFiltre.DiscountType).ToList();
            }
            if (PaginationFiltre.CategoryId != null)
            {
                Promotions = Promotions.Where(c => c.CategoryId == PaginationFiltre.CategoryId).ToList();
            }
            if (PaginationFiltre.PromotionNameEn != null)
            {
                Promotions = Promotions.Where(c => c.PromotionNameEn.ToUpper() == PaginationFiltre.PromotionNameEn.ToUpper()).ToList();
            }
            if (PaginationFiltre.PromotionNameAr != null)
            {
                Promotions = Promotions.Where(c => c.PromotionNameAr.ToUpper() == PaginationFiltre.PromotionNameAr.ToUpper()).ToList();
            }

            int totalCount = await DbSet.Promotions.CountAsync();
            Promotions = Promotions.Skip(PaginationFiltre.ItemsPerPage * (PaginationFiltre.PageNumber - 1))
                                      .Take(PaginationFiltre.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
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
        //Get One Category
        public async Task<PromotionDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Promotions.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var promotionDto = _mapper.Map<PromotionDto>(entityToFind);
                return promotionDto;
            }
            return null;
        }

        //Get Product By CategoryId(fils)
        public async Task<ICollection<PromotionDto>> GetByCategoryIdAsync(int CategoryId)
        {
            var productsToFind = await DbSet.Promotions
                .Where(p => p.CategoryId == CategoryId && !p.IsDeleted).ToListAsync();
            var promotionDto = _mapper.Map<List<PromotionDto>>(productsToFind);
            return promotionDto;
        }

        //Get Product By CategoryId(fils)
        public async Task<ICollection<PromotionDto>> GetByMerchantIdAsync(int merchantId)
        {
            var productsToFind = await DbSet.Promotions
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var promotionDto = _mapper.Map<List<PromotionDto>>(productsToFind);
            return promotionDto;
        }
    }
}
