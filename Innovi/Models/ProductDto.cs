using Innovi.Entities;
using System.Text.Json.Serialization;

namespace Innovi.Models
{
    public class ProductDto
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? ProductCode { get; set; } = null!;

        public string? ProductNameEn { get; set; } = null!;

        public string? ProductNameAr { get; set; } = null!;

        public string? ProductDescriptionEn { get; set; } = null!;

        public string? ProductDescriptionAr { get; set; } = null!;

        public int? CategoryId { get; set; }

        public int? ManufacturerId { get; set; }

        public int? SupplierId { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Price { get; set; }

        public int? MinimumQuantity { get; set; }

        public int? OutOfStockQuantity { get; set; }

        public string? ThumbnailImage { get; set; }

        public bool? ShowInHomePage { get; set; }

        public bool? MarkAsNew { get; set; }

        public bool? IsHuge { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsActive { get; set; }

        public int? MerchantId { get; set; }

        public string? ProductLocation { get; set; }

        public int? StorageId { get; set; }

        public string? SupplierProductCode { get; set; }

        public string? VideoUrl { get; set; }

        public bool? IsAvailableForPickup { get; set; }

        [JsonIgnore]
        public virtual CategoryDto? Category { get; set; } = null!;
        [JsonIgnore]
        public virtual MerchantDto? Merchant { get; set; } = null!;
        [JsonIgnore]
        public virtual ManufacturerDto? Manufacturer { get; set; } = null!;
        [JsonIgnore]
        public virtual StorageDto Storage { get; set; } = null!;
        [JsonIgnore]
        public virtual SupplierDto Supplier { get; set; } = null!;
        public virtual ICollection<ProductImageDto>? ProductImages { get; set; } = new List<ProductImageDto>();

        public virtual ICollection<ProductQuantityDto>? ProductQuantities { get; set; } = new List<ProductQuantityDto>();

        public virtual ICollection<TagDto> Tags { get; set; } = new List<TagDto>();

        public virtual ICollection<ProductSpecseDto> ProductSpecses { get; set; } = new List<ProductSpecseDto>();

        public virtual ICollection<ProductAttributesValueDto> ProductAttributesValues { get; set; } = new List<ProductAttributesValueDto>();

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();





    }
}
