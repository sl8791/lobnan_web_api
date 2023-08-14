using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface IPromotionRepository
    {
        Task<PromotionDto> GetByIdAsync(int id);
        Task<ICollection<PromotionDto>> GetAllAsync();
        Task<ICollection<PromotionDto>> GetByCategoryIdAsync(int CategoryId);
        Task<ICollection<PromotionDto>> GetByMerchantIdAsync(int MerchantId);
        Task<CountListData<PromotionDto>> GetWithPagination(PromotionFilterDto PaginationFiltre);
    }
}
