using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int? id);
    }
}
