using System.Text.Json.Serialization;
namespace Innovi.Models
{
    public class SwipeBannerDto
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public string TitleEn { get; set; } = null!;

        public string TitleAr { get; set; } = null!;

        public string? DescriptionEn { get; set; }

        public string? DescriptionAr { get; set; }

        public string? UrlEn { get; set; }

        public string? UrlAr { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public string ImageAr { get; set; } = null!;

        public string ImageEn { get; set; } = null!;

        [JsonIgnore]
        public virtual CategoryDto? Category { get; set; } = null!;
    }
}
