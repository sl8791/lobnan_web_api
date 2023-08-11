using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class UsefulLink
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string PageNameEn { get; set; } = null!;

    public string PageNameAr { get; set; } = null!;

    public string ContentEn { get; set; } = null!;

    public string ContentAr { get; set; } = null!;

    public bool IsActive { get; set; }
}
