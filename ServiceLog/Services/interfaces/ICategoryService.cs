using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;

namespace ServiceLog.Services.interfaces
{
    public interface ICategoryService
    {
        Task<GetAllCategoryResponseDto> GetAllCategoriesAsync();
        Task<GetByIdCategoryResponseDto> GetCategoryByIdAsync(string id);
        Task<NewCategoryResponseDto> CreateCategoryAsync(NewCategoryRequestDto newCategoryRequestDto);
        Task<UpdateCategoryResponseDto> UpdateCategoryAsync(string id, UpdateCategoryRequestDto updateCategoryRequestDto);
        Task<DeleteCategoryResponseDto> DeleteCategoryAsync(string id);
        //Task<List<Category>> GetCategoriesByServiceOptionAsync(string serviceOption);
    }
}
