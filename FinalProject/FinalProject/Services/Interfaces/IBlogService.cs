using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAll();
        Task<Blog> GetById(int? id);
    }
}
