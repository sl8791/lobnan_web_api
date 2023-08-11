using Innovi.Models;

namespace Innovi.Services.Interfaces
{
    public interface ICategorieRepository 

    {
        Task<CategoryDto> CreateAsync(CategoryDto category);
        Task<bool> RemoveAsync(int id);
        Task<CategoryDto> UpdateAsync(CategoryDto category);
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> GetAsync(CategoryDto filter);
        Task<ICollection<CategoryDto>> GetAllAsync();
        Task<ICollection<CategoryDto>> GetAllAsync(CategoryDto filter);
        Task<ICollection<CategoryDto>> GetByParentCategoryIdAsync(int parentCategoryId);
        Task<CountListData<CategoryDto>> GetWithPagination(CategoryFilterDto PaginationFiltre);
      
    }
}
