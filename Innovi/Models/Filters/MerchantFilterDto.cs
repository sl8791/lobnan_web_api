namespace Innovi.Models.Filters
{
    public class MerchantFilterDto : MerchantDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
