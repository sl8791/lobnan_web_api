using System.Text.Json.Serialization;
using Innovi.Entities;

namespace Innovi.Models
{
    public class ProductQuantityDto
    {
        public int? Id { get; set; }

        public int? ProductId { get; set; }

        public int? BranchId { get; set; }

        public int? AttributeValueId1 { get; set; }

        public int? AttributeValueId2 { get; set; }

        public int? AttributeValueId3 { get; set; }

        public decimal? Quantity { get; set; }

        [JsonIgnore]
        public virtual BranchDto? Branch { get; set; } = null!;
        [JsonIgnore]
        public virtual ProductDto? Product { get; set; } = null!;
    }
}
