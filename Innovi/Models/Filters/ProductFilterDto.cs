namespace Innovi.Models.Filters
{
    public class ProductFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? ProductCode { get; set; }
        public string? ProductNameAr { get; set; }
        public string? ProductNameEn { get; set; }
        public string? Price { get; set; }
        public string? CategoryId { get; set; }
    }
}
