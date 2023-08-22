using FinalProject.Areas.Admin.ViewModels.DessertMenu;
using FinalProject.Areas.Admin.ViewModels.StarterMenu;
using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IDessertMenuService
    {
  
        Task<List<Dessert>> GetAll();
        Task<Dessert> GetDessertMenuById(int? id);
        Task<DessertMenuImage> GetImageById(int? id);
        Task<List<FoodComment>> GetComments();
        Task<FoodComment> GetCommentById(int? id);
        Task<FoodComment> GetCommentByIdWithProduct(int? id);

        Task<List<Dessert>> GetPaginatedDatasAsync(int page, int take);
        Task<Dessert> GetByIdWithImagesAsync(int? id);
        List<Areas.Admin.ViewModels.DessertMenu.DessertMenuVM> GetMappedDatas(List<Dessert> products);
        Task<Dessert> GetWithIncludesAsync(int id);
        DessertMenuDetailVM GetMappedData(Dessert product);
        Task<int> GetCountAsync();
        Task CreateAsync(DessertMenuCreateVM model);
        Task DeleteImageByIdAsync(int id);
        Task EditAsync(int productId, DessertMenuEditVM model);
        Task DeleteAsync(int id);
    }
}
