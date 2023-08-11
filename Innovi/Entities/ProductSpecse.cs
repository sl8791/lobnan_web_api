using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class ProductSpecse
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductId { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string ValueEn { get; set; } = null!;

    public string ValueAr { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public virtual Product Product { get; set; } = null!;
}
