using AutoMapper;
using Innovi.Data;
using Innovi.Models.Filters;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Innovi.Entities;


namespace Innovi.Services.Repository
{
    public class BranchRepository: IBranchRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public BranchRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Branchs
        public async Task<ICollection<BranchDto>> GetAllAsync()
        {
            try
            {
                var branch = await DbSet.Branches.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var branchs = branch.ToList();
                List<BranchDto> cats = new List<BranchDto>();
                foreach (var item in branchs)
                {
                    BranchDto c = new BranchDto();
                    c = _mapper.Map<BranchDto>(item);
                    cats.Add(c);
                }
                if (cats.Count == 0)
                {
                    return null;
                }
                return cats;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Filter With Pagination
        public async Task<CountListData<BranchDto>> GetWithPagination(BranchFilterDto PaginationFilter)
        {
            var BranchByPage = await DbSet.Branches.Where(p => p.IsDeleted == false).ToListAsync();
            var Branchs = BranchByPage.ToList();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CityId", PaginationFilter.CityId);
            dic.Add("MerchantId", PaginationFilter.MerchantId);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "CityId":
                        if (item.Value != null)
                            Branchs = Branchs.Where(c => c.CityId == int.Parse(PaginationFilter.CityId)).ToList();
                        break;
                    case "MerchantId":
                        if (item.Value != null)
                            Branchs = Branchs.Where(c => c.MerchantId == int.Parse(PaginationFilter.MerchantId)).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            Branchs = Branchs.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            Branchs = Branchs.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
           int totalCount = await DbSet.Branches.CountAsync();
            Branchs = Branchs.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                      .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
            List<BranchDto> cats = new List<BranchDto>();
            foreach (var item in Branchs)
            {
                cats.Add(_mapper.Map<BranchDto>(item));
            }
            if (cats.Count == 0)
            {
                return null;
            }
            var countListData = new CountListData<BranchDto>(cats, totalCount);
            return countListData;
        }
        //Get One Branch
        public async Task<BranchDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Branches.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                var branchDto = _mapper.Map<BranchDto>(entityToFind);
                return branchDto;
            }
            return null;
        }

        //Get Branch By City
        public async Task<ICollection<BranchDto>> GetByCityIdAsync(int CityId)
        {
            var entityToFind = await DbSet.Branches
                .Where(p => p.CityId == CityId && !p.IsDeleted).ToListAsync();
            var branchDto = _mapper.Map<List<BranchDto>>(entityToFind);
            return branchDto;
        }
        //Get Branch By MerchantId
        public async Task<ICollection<BranchDto>> GetByMerchantIdAsync(int merchantId)
        {
            var entityToFind = await DbSet.Branches
                .Where(p => p.MerchantId == merchantId && !p.IsDeleted).ToListAsync();
            var branchDto = _mapper.Map<List<BranchDto>>(entityToFind);
            return branchDto;
        }
    }
}
