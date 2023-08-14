using System.Text.Json.Serialization;
using Innovi.Entities;

namespace Innovi.Models
{
    public class BranchDto 
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public int? CountryId { get; set; }

        public int? GovernorateId { get; set; }

        public int? CityId { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public int? MerchantId { get; set; }

        public bool? IsForOnlineSelling { get; set; }
        [JsonIgnore]
        public virtual CityDto? City { get; set; } = null!;
        [JsonIgnore]
        public virtual MerchantDto? Merchant { get; set; } = null!;

        //public virtual ICollection<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
