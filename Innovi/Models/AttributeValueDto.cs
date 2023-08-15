using Innovi.Entities;

namespace Innovi.Models
{
    public class AttributeValueDto 
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public string? Image { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public int? AttributeId { get; set; }

        public virtual AttributeDto? Attribute { get; set; } = null!;

        public virtual ICollection<ProductAttributesValueDto>? ProductAttributesValues { get; set; } = new List<ProductAttributesValueDto>();
    }
}
