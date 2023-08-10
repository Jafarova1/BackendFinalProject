using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAboutBannerService
    {
        Task<List<AboutBanner>> GetAll();
        Task<AboutBanner> GetAboutBannerById(int? id);
    }
}
