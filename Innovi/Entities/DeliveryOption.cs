using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class DeliveryOption
{
    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public decimal FreeDeliveryAmount { get; set; }

    public decimal Cost { get; set; }

    public bool IsActive { get; set; }

    public string Image { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public int Id { get; set; }

    public bool? ShowForCustomers { get; set; }

    public string? DescriptionAr { get; set; }

    public string? DescriptionEn { get; set; }

    public bool? ShowAsNote { get; set; }
}
