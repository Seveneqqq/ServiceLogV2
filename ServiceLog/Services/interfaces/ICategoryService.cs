using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;

namespace ServiceLog.Services.interfaces
{
    public interface ICategoryService
    {
        Task<GetAllCategoryDto> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task<NewCategoryResponseDto> CreateCategoryAsync(NewCategoryRequestDto newCategoryRequestDto);
        Task<Category> UpdateCategoryAsync(string id, Category category);
        Task DeleteCategoryAsync(string id);
        //Task<List<Category>> GetCategoriesByServiceOptionAsync(string serviceOption);
    }
}
