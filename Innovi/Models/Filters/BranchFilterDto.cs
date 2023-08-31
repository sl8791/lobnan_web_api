namespace Innovi.Models.Filters
{
    public class BranchFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? CityId { get; set; }
        public string? MerchantId { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
    }
}
