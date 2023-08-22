using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class SettingService:ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetAll()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }

        public async Task<Setting> GetSettingByIdAsync(int? id) => await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<List<Setting>> GetSettingsAsync() => await _context.Settings.ToListAsync();

    }
}
