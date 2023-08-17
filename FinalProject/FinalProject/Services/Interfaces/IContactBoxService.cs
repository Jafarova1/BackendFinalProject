using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IContactBoxService
    {
        Task<List<ContactBox>> GetAll();
        Task<ContactBox> GetAboutBoxById(int? id);
    }
}
