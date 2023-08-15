using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IStarterMenuService
    {
        Task<List<Starter>> GetAll();
        Task<Starter> GetStarterMenuById(int? id);
        Task<StarterMenuImage> GetImageById(int? id);
        Task<List<FoodComment>> GetComments();
        Task<FoodComment> GetCommentById(int? id);
        Task<FoodComment> GetCommentByIdWithProduct(int? id);

    }
}
