using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class ShippingAddress
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public int CountryId { get; set; }

    public int GovernorateId { get; set; }

    public int CityId { get; set; }

    public bool IsDefaultAddress { get; set; }

    public string Building { get; set; } = null!;

    public string Flat { get; set; } = null!;

    public string Floor { get; set; } = null!;

    public string? MoreInformation { get; set; }

    public string Street { get; set; } = null!;

    public string? Area { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Governorate Governorate { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual AspNetUser User { get; set; } = null!;
}
