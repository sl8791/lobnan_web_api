namespace Innovi.Models.Filters
{
    public class CountryFilterDto: MerchantDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
