using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class SocialService:ISocialService
    {
        private readonly AppDbContext _context;

        public SocialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Social>> GetAll() => await _context.Socials.ToListAsync();
        public async Task<Social> GetSocialById(int? id) => await _context.Socials.FirstOrDefaultAsync(m => m.Id == id);
    }
}
