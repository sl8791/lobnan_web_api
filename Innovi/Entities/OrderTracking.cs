using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class OrderTracking
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int OrderId { get; set; }

    public int OrderStatusId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual OrderStatus OrderStatus { get; set; } = null!;
}
