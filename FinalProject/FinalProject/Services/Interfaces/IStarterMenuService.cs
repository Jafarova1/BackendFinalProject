using FinalProject.Areas.Admin.ViewModels.StarterMenu;
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


        Task<List<Starter>> GetPaginatedDatasAsync(int page, int take);
        Task<Starter> GetByIdWithImagesAsync(int? id);
        List<Areas.Admin.ViewModels.StarterMenu.StarterMenuVM> GetMappedDatas(List<Starter> products);
        Task<Starter> GetWithIncludesAsync(int id);
        StarterMenuDetailVM GetMappedData(Starter product);
        Task<int> GetCountAsync();
        Task CreateAsync(StarterMenuCreateVM model);
        Task DeleteImageByIdAsync(int id);
        Task EditAsync(int productId, StarterMenuEditVM model);
        Task DeleteAsync(int id);

    }
}
