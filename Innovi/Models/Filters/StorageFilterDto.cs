namespace Innovi.Models.Filters
{
    public class StorageFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? MerchantId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
