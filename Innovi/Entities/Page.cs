using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Page
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public int GroupNumber { get; set; }

    public int MenuGroup { get; set; }

    public virtual ICollection<RolesPage> RolesPages { get; set; } = new List<RolesPage>();
}
