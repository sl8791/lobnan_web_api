﻿using Innovi.Entities;

namespace Innovi.Models
{
    public class SupplierDto 
    {
        public int? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; } = null!;

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NameEn { get; set; } = null!;

        public string? NameAr { get; set; } = null!;

        public string? AddressEn { get; set; } = null!;

        public string? AddressAr { get; set; } = null!;

        public string? PhoneNo1 { get; set; } = null!;

        public string? PhoneNo2 { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
    }
}
