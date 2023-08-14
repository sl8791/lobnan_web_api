namespace Innovi.Models.Filters
{
    public class CityFilterDto:CityDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
