using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IStarterMenuService
    {   Task<IEnumerable<Starter>> GetAll();
        Task<Starter> GetById(int? id);
        Task<int> GetCountAsync();
        Task<List<StarterMenuVM>> GetMappedAllFoods();
        Task<List<Starter>> GetFeaturedFoods();
        Task<List<Starter>> GetLatestFoods();
        Task<StarterMenuImage> GetImageById(int? id);
        Task<Starter> GetFoodByImageId(int? id);

    }
}
