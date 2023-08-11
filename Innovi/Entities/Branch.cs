using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Branch
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public int CountryId { get; set; }

    public int GovernorateId { get; set; }

    public int CityId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int MerchantId { get; set; }

    public bool? IsForOnlineSelling { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual ICollection<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
