using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string? AttributeValueIds { get; set; }

    public decimal ItemPrice { get; set; }

    public int Quantity { get; set; }

    public int? BranchId { get; set; }

    public decimal TotalAttributesAmount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
