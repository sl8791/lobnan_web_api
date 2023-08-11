namespace Innovi.Models
{
    public class PromotionFilterDto: PromotionDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
