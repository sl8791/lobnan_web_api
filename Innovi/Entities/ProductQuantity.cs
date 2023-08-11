using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class ProductQuantity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int BranchId { get; set; }

    public int AttributeValueId1 { get; set; }

    public int? AttributeValueId2 { get; set; }

    public int? AttributeValueId3 { get; set; }

    public decimal Quantity { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
