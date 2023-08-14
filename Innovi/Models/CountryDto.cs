using System.Text.Json.Serialization;
using Innovi.Entities;

namespace Innovi.Models
{
    public class CountryDto
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;
        public virtual ICollection<MerchantDto>? Merchants { get; set; } = new List<MerchantDto>();
        public virtual ICollection<CityDto>? Cities { get; set; } = new List<CityDto>();
        public virtual ICollection<GovernorateDto>? Governorates { get; set; } = new List<GovernorateDto>();

        //public virtual ICollection<RegisteredMerchant> RegisteredMerchants { get; set; } = new List<RegisteredMerchant>();

        //public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
    }
}
