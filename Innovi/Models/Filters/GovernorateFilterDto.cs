namespace Innovi.Models.Filters
{
    public class GovernorateFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? CountryId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
