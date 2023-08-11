using Innovi.Entities;
using System.Text.Json.Serialization;

namespace Innovi.Models
{
    public class CategoryDto
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Code { get; set; } = null!;

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public int? ParentCategoryId { get; set; }

        public bool? AddToHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }
        public virtual CategoryDto? ParentCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
        //ToVerif
        //[JsonIgnore]
        //public virtual ICollection<CategoryDto> InverseParentCategory { get; set; } = new List<CategoryDto>();
        [JsonIgnore]
        public virtual ICollection<PromotionDto> Promotions { get; set; } = new List<PromotionDto>();


    }
}
