namespace Innovi.Models
{
    public class ProductFilterDto : ProductDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
