using System.Text.Json.Serialization;
using Innovi.Entities;

namespace Innovi.Models
{
    public class ProductImageDto 
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public int? ProductId { get; set; }

        public string? FileName { get; set; } = null!;

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }
        [JsonIgnore]
        public virtual ProductDto? Product { get; set; } = null!;
    }
}
