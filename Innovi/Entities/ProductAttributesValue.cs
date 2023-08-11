using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class ProductAttributesValue
{
    public int ProductId { get; set; }

    public int AttributeValueId { get; set; }

    public int AttributeId { get; set; }

    public decimal ExtraCost { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual AttributeValue AttributeValue { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
