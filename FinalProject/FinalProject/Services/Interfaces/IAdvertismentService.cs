using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAdvertismentService
    {
        Task<List<Advertisment>> GetAll();
        Task<Advertisment> GetAdvertisingById(int? id);
    }
}
