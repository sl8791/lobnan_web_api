using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class RegisteredMerchant
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Phonenumber { get; set; } = null!;

    public string? Email { get; set; }

    public int CountryId { get; set; }

    public int GovernorateId { get; set; }

    public int CityId { get; set; }

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public string Floor { get; set; } = null!;

    public string Flat { get; set; } = null!;

    public string? MoreInformation { get; set; }

    public string CategoriesOfWork { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? Website { get; set; }

    public string? Area { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual Governorate Governorate { get; set; } = null!;
}
