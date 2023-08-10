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
        public async Task<List<Subscriber>> GetAll() => await _context.Subscribers.ToListAsync();

        public async Task<Subscriber> GetSubscribeById(int? id) => await _context.Subscribers.FirstOrDefaultAsync(m => m.Id == id);
    }
}
