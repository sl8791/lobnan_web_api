using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public string? NameAr { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<RolesPage> RolesPages { get; set; } = new List<RolesPage>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
