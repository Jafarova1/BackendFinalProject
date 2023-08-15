using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IDessertMenuService
    {
        //Task<IEnumerable<Dessert>> GetAll();
        //Task<Dessert> GetById(int? id);
        //Task<int> GetCountAsync();
        //Task<List<DessertMenuVM>> GetMappedAllFoods();
        //Task<List<Dessert>> GetFeaturedFoods();
        //Task<List<Dessert>> GetLatestFoods();
        //Task<DessertMenuImage> GetImageById(int? id);
        //Task<Dessert> GetFoodByImageId(int? id);
        Task<List<Dessert>> GetAll();
        Task<Dessert> GetDessertMenuById(int? id);
        Task<DessertMenuImage> GetImageById(int? id);
        Task<List<FoodComment>> GetComments();
        Task<FoodComment> GetCommentById(int? id);
        Task<FoodComment> GetCommentByIdWithProduct(int? id);
    }
}
