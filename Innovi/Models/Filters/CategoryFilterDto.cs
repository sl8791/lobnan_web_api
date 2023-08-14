namespace Innovi.Models.Filters
{
    public class CategoryFilterDto : CategoryDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
