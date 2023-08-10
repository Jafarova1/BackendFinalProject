using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAboutSliderService
    {
        Task<List<AboutSlider>> GetAll();
        Task<AboutSlider> GetAboutSliderById(int? id);
    }
}
