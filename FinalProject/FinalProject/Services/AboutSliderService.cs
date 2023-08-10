using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AboutSliderService:IAboutSliderService
    {
        private readonly AppDbContext _context;

        public AboutSliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AboutSlider>> GetAll() => await _context.AboutSliders.ToListAsync();

        public async Task<AboutSlider> GetAboutSliderById(int? id) => await _context.AboutSliders.FirstOrDefaultAsync(m => m.Id == id);


    }
}
