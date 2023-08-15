namespace Innovi.Models.Filters
{
    public class AttributeFilterDto: AttributeDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
