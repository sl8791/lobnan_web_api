namespace Innovi.Models.Filters
{
    public class ProductSpecseFilterDto: ProductSpecseDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
