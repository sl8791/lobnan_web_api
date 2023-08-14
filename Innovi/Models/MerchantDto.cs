using System.Text.Json.Serialization;
using Innovi.Entities;

namespace Innovi.Models
{
    public class MerchantDto
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public string? AddressEn { get; set; } = null!;

        public string? AddressAr { get; set; } = null!;

        public string? PhoneNo1 { get; set; } = null!;

        public string? PhoneNo2 { get; set; }

        public bool? IsActive { get; set; }

        public int? CountryId { get; set; }

        public string? Email { get; set; } = null!;
        [JsonIgnore]
        public virtual CountryDto? Country { get; set; } = null!;
        public virtual ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
        public virtual ICollection<PromotionDto>? Promotions { get; set; } = new List<PromotionDto>();
        public virtual ICollection<BranchDto>? Branches { get; set; } = new List<BranchDto>();
        //public virtual ICollection<StorageDto> Storages { get; set; } = new List<StorageDto>();
        //public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();
        // public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();


    }
}
