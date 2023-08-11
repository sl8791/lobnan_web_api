using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class RolesPage
{
    public string RoleId { get; set; } = null!;

    public int PageId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public bool? HasAuthorization { get; set; }

    public virtual Page Page { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;
}
