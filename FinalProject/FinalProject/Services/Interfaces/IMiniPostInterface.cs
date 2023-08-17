using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IMiniPostInterface
    {
        Task<List<MiniPost>> GetAll();
        Task<int> GetCountAsync();
        Task<List<MiniPost>> GetPaginatedDatas(int page, int take);
    }
}
