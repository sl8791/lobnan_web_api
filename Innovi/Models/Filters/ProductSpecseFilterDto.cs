namespace Innovi.Models.Filters
{
    public class ProductSpecseFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? ProductId { get; set; }
        public string? ValueAr { get; set; }
        public string? ValueEn { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
    }
}
