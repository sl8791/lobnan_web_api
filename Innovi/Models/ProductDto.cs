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

        //public virtual Manufacturer Manufacturer { get; set; } = null!;

        //public virtual Merchant Merchant { get; set; } = null!;

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        //public virtual ICollection<ProductAttributesValue> ProductAttributesValues { get; set; } = new List<ProductAttributesValue>();

        //public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        //public virtual ICollection<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

        //public virtual ICollection<ProductSpecse> ProductSpecses { get; set; } = new List<ProductSpecse>();

        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        //public virtual Storage Storage { get; set; } = null!;

        //public virtual Supplier Supplier { get; set; } = null!;

        //public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
