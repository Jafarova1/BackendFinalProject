using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<List<AboutUs>> GetAll();
        Task<AboutUs> GetAboutUsById(int? id);
    }
}
