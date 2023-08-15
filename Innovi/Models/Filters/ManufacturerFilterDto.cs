namespace Innovi.Models.Filters
{
    public class ManufacturerFilterDto: ManufacturerDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
