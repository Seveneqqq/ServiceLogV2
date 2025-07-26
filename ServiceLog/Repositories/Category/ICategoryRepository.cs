using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLog.Models.Domain;

namespace ServiceLog.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(string id);
        Task CreateCategoryAsync(Category category);
        Task<ReplaceOneResult> UpdateCategoryAsync(string id, Category category);
        Task<DeleteResult> DeleteCategoryAsync(string id);
    }
}
