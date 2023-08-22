using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IRecentBlogService
    {
        Task<List<RecentBlog>> GetAll();
        Task<RecentBlog> GetById(int? id);
    }
}
