using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AdvertismentService : IAdvertismentService
    {
        private readonly AppDbContext _context;

        public AdvertismentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Advertisment>> GetAll() => await _context.Advertisments.ToListAsync();

        public async Task<Advertisment> GetAdvertisingById(int? id) => await _context.Advertisments.FirstOrDefaultAsync(m => m.Id == id);
    }
}
