using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Services
{
    public class LayoutService 

    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<Dictionary<string, string>> GetSettingDatas()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
        public async Task<List<Social>> GetAll() => await _context.Socials.ToListAsync();

    
    }
}
