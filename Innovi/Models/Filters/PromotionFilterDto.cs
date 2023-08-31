namespace Innovi.Models.Filters
{
    public class PromotionFilterDto 
    {
        public int PageNumber { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 5;
        public string? PromotionCode { get; set; }
        public string? DiscountType { get; set; }
        public string? CategoryId { get; set; }
        public string? PromotionNameEn { get; set; }
        public string? PromotionNameAr { get; set; }
    }
}
