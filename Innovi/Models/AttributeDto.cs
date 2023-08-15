using Innovi.Entities;

namespace Innovi.Models
{
    public class AttributeDto   
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public int? DisplayType { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public int? MerchantId { get; set; }

        public bool? IsDefault { get; set; }

        public virtual MerchantDto? Merchant { get; set; } = null!;
        public virtual ICollection<ProductAttributesValueDto>? ProductAttributesValues { get; set; } = new List<ProductAttributesValueDto>();
        public virtual ICollection<AttributeValueDto>? AttributeValues { get; set; } = new List<AttributeValueDto>();
    }
}
