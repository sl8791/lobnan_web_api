namespace Innovi.Models.Filters
{
    public class ManufacturerFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
