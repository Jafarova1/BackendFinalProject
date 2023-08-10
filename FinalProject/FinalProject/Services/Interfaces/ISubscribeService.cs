using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task<List<Subscriber>> GetAll();
        Task<Subscriber> GetSubscribeById(int? id);
    }
}
