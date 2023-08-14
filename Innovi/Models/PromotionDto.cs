using Innovi.Entities;
using System.Text.Json.Serialization;

namespace Innovi.Models
{
    public class PromotionDto
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public int? MerchantId { get; set; }

        public int? CategoryId { get; set; }

        public string? PromotionCode { get; set; } = null!;

        public string? PromotionNameEn { get; set; } = null!;

        public string? PromotionNameAr { get; set; } = null!;

        public string? PromotionDescriptionEn { get; set; } = null!;

        public string? PromotionDescriptionAr { get; set; } = null!;

        public int? DiscountType { get; set; }

        public decimal? DiscountValue { get; set; }

        public int? PromotionType { get; set; }

        public decimal? MinimumAmount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }
        [JsonIgnore]
        public virtual CategoryDto? Category { get; set; } = null!;
        [JsonIgnore]
        public virtual MerchantDto? Merchant { get; set; } = null!;

    }
}
