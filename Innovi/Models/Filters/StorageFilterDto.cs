namespace Innovi.Models.Filters
{
    public class StorageFilterDto: StorageDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
