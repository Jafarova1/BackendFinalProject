using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AboutBannerService:IAboutBannerService
    {
        private readonly AppDbContext _context;

        public AboutBannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AboutBanner>> GetAll() => await _context.AboutBanners.ToListAsync();

        public async Task<AboutBanner> GetAboutBannerById(int? id) => await _context.AboutBanners.FirstOrDefaultAsync(m => m.Id == id);

    }
}
