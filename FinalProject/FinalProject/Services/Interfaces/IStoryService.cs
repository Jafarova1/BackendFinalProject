using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IStoryService
    {
        Task<List<Story>> GetAll();
        Task<int> GetCountAsync();
        Task<List<Story>> GetPaginatedDatas(int page, int take);
        //Task<Story> GetById(int? id);

    }
}
