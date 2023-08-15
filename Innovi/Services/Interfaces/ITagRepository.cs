using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ITagRepository
    {
        Task<TagDto> GetByIdAsync(int id);
        Task<ICollection<TagDto>> GetAllAsync();
    }
}
