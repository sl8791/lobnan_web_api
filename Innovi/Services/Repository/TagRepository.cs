using AutoMapper;
using Innovi.Data;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Innovi.Services.Repository
{
    public class TagRepository: ITagRepository
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;
        public TagRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }
        //Get All Tags
        public async Task<ICollection<TagDto>> GetAllAsync()
        {
            try
            {
                var Tag = await DbSet.Tags.ToListAsync();
                var Tags = Tag.ToList();
                List<TagDto> cats = new List<TagDto>();
                foreach (var item in Tags)
                {
                    TagDto c = new TagDto();
                    c = _mapper.Map<TagDto>(item);
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
        //Get One Tag
        public async Task<TagDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Tags.FindAsync(id);
            if (entityToFind != null)
            {
                var storageDto = _mapper.Map<TagDto>(entityToFind);
                return storageDto;
            }
            return null;
        }
    }
}
