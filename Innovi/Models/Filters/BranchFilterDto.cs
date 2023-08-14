namespace Innovi.Models.Filters
{
    public class BranchFilterDto: BranchDto
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
