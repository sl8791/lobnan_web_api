namespace Innovi.Models.Filters
{
    public class SupplierFilterDto
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? AddressEn { get; set; }
        public string? AddressAr { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }       
    }
}
