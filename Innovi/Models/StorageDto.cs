using Innovi.Entities;
using System.Text.Json.Serialization;


namespace Innovi.Models
{
    public class StorageDto
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

        public bool? IsActive { get; set; }

        public int? MerchantId { get; set; }

        [JsonIgnore]
        public virtual MerchantDto? Merchant { get; set; } = null!;

        public virtual ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
    }
}
