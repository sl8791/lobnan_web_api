using System;
using System.Collections.Generic;

using Innovi.Entities;


namespace Innovi.Models
{
    public class ProductAttributesValueDto 
    {
        public int? ProductId { get; set; }

        public int? AttributeValueId { get; set; }

        public int? AttributeId { get; set; }

        public decimal? ExtraCost { get; set; }

        public virtual ProductDto? Product { get; set; } = null!;
        public virtual AttributeDto? Attribute { get; set; } = null!;
        public virtual AttributeValueDto? AttributeValue { get; set; } = null!;
    }
}
