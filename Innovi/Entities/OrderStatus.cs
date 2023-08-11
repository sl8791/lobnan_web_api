using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class OrderStatus
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public bool DisplayToCustomer { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
}
