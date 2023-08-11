using System;
using System.Collections.Generic;

namespace Innovi.Entities;

public partial class SysConfig
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; }

    public string? Twitter { get; set; }

    public string? Facebook { get; set; }

    public string? Instagram { get; set; }
}
