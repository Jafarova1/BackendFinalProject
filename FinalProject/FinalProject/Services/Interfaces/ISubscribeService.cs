using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscriber>> GetAllAsync();
        Task<Subscriber> GetByIdAsync(int? id);
    }
}
