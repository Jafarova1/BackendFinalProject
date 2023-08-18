using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAdditionalFoodService
    {
        Task<List<AdditionalFood>> GetAll();
        Task<int> GetCountAsync();
        Task<List<AdditionalFood>> GetPaginatedDatas(int page, int take);
    }
}
