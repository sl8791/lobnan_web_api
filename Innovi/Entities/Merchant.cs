using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Merchant
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string AddressEn { get; set; } = null!;

    public string AddressAr { get; set; } = null!;

    public string PhoneNo1 { get; set; } = null!;

    public string? PhoneNo2 { get; set; }

    public bool IsActive { get; set; }

    public int CountryId { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
}
