using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Ad
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string TitleEn { get; set; } = null!;

    public string TitleAr { get; set; } = null!;

    public string? DescriptionEn { get; set; }

    public string? DescriptionAr { get; set; }

    public string Image { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }

    public bool IsActive { get; set; }

    public string? UrlAr { get; set; }

    public string? UrlEn { get; set; }
}
