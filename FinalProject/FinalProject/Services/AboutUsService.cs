using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AboutUsService:IAboutUsService
    {
        private readonly AppDbContext _context;

        public AboutUsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AboutUs>> GetAll() => await _context.AboutUss.ToListAsync();

        public async Task<AboutUs> GetAboutUsById(int? id) => await _context.AboutUss.FirstOrDefaultAsync(m => m.Id == id);
    }
}
