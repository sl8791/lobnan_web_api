namespace Innovi.Models.Filters
{
    public class SupplierFilterDto: SupplierDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
