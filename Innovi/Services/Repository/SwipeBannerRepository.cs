using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Innovi.Services.Repository
{
    public class SwipeBannerRepository: ISwipeBannerRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public SwipeBannerRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All SwipeBanner
        public async Task<ICollection<SwipeBannerDto>> GetAllAsync()
        {
            try
            {
                var SwipeBanner = await DbSet.SwipeBanners.Where(p => p.IsDeleted == false).OrderByDescending(p => p.CreatedOn).ToListAsync();
                var SwipeBanners = SwipeBanner.ToList();
                List<SwipeBannerDto> LSB = new List<SwipeBannerDto>();
                foreach (var item in SwipeBanners)
                {
                    SwipeBannerDto c = new SwipeBannerDto();
                    c = _mapper.Map<SwipeBannerDto>(item);
                    LSB.Add(c);
                }
                if (LSB.Count == 0)
                {
                    return null;
                }
                return LSB;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }
        }
        //Get One SwipeBanner
        public async Task<SwipeBannerDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.SwipeBanners.FindAsync(id);
            if (!entityToFind.IsDeleted)
            {
                var swipeBannerDto = _mapper.Map<SwipeBannerDto>(entityToFind);
                return swipeBannerDto;
            }
            return null;
        }

        //Get SwipeBanner By CategoryId(fils)
        public async Task<ICollection<SwipeBannerDto>> GetByCategoryIdAsync(int CategoryId)
        {
            var entityToFind = await DbSet.SwipeBanners
                .Where(p => p.CategoryId == CategoryId && !p.IsDeleted).ToListAsync();
            var swipeBannerDto = _mapper.Map<List<SwipeBannerDto>>(entityToFind);
            return swipeBannerDto;
        }
    }
}
