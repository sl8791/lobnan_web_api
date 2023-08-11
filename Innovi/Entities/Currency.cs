﻿using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class Currency
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string NameEn { get; set; } = null!;

    public string NameAr { get; set; } = null!;

    public string CodeEn { get; set; } = null!;

    public string CodeAr { get; set; } = null!;

    public int DecimalDigits { get; set; }

    public bool IsDefault { get; set; }
}
