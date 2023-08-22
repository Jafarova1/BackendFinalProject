using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface ISettingService
    {
        Dictionary<string, string> GetAll();
        Task<Setting> GetSettingByIdAsync(int? id);
        Task<List<Setting>> GetSettingsAsync();
    }
}
