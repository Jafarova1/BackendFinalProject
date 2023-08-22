using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IMiniPostService
    {
        Task<List<MiniPost>> GetAll();
        Task<int> GetCountAsync();
        Task<List<MiniPost>> GetPaginatedDatas(int page, int take);
        Task<MiniPost> GetById(int? id);
    }
}
