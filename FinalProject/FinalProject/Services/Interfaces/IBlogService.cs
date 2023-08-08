using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<List<BlogVM>> GetAllMappedDatasAsync();
        Task CreateAsync(List<IFormFile> images);
        Task DeleteAsync(int id);
        Task EditAsync(Blog blog, IFormFile newImage);
    }
}
