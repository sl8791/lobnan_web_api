namespace Innovi.Models.Filters
{
    public class CategoryFilterDto 
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? Code { get; set; }

        public string? NameEn { get; set; }

        public string? NameAr { get; set; }
    }
}
