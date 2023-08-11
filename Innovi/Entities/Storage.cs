using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Storage
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string Code { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public bool IsActive { get; set; }

    public int MerchantId { get; set; }

    public virtual Merchant Merchant { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
