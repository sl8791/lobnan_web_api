using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class ProductImage
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductId { get; set; }

    public string FileName { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual Product Product { get; set; } = null!;
}
