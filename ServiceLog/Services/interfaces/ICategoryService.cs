using ServiceLog.Models.Domain;

namespace ServiceLog.Services.interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(string id, Category category);
        Task DeleteCategoryAsync(string id);
        //Task<List<Category>> GetCategoriesByServiceOptionAsync(string serviceOption);
    }
}
