using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class AttributeValue
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string? Image { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int AttributeId { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual ICollection<ProductAttributesValue> ProductAttributesValues { get; set; } = new List<ProductAttributesValue>();
}
