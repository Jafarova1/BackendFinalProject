using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetCategoryById(int? id);
    }
}
