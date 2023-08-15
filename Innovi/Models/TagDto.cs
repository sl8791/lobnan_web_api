using Innovi.Entities;

namespace Innovi.Models
{
    public class TagDto 
    {
        public int? Id { get; set; }

        public string? Name { get; set; } = null!;

        public virtual ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
    }
}
