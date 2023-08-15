namespace Innovi.Models.Filters
{
    public class AttributeValueFilterDto: AttributeValueDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
