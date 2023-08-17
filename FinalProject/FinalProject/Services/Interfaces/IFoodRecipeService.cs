using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IFoodRecipeService
    {
        Task<List<News>> GetAll();
        Task<News> GetById(int? id);
        Task<List<FoodComment>> GetComments();
        Task<FoodComment> GetCommentById(int? id);
        Task<FoodComment> GetCommentByIdWithNews(int? id);
    }
}
