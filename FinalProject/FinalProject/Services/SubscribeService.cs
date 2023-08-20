using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;

        public SubscribeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Subscriber>> GetAllAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }
        public async Task<Subscriber> GetByIdAsync(int? id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
