using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Attribute
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public int DisplayType { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int MerchantId { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual ICollection<ProductAttributesValue> ProductAttributesValues { get; set; } = new List<ProductAttributesValue>();
}
