using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ISocialService
    {
        Task<List<Social>> GetAll();
        Task<Social> GetSocialById(int? id);
    }
}
