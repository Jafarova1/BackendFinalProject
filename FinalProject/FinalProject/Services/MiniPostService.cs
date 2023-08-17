using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class MiniPostService : IMiniPostInterface
    {
        private readonly AppDbContext _context;

        public MiniPostService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<MiniPost>> GetAll() => await _context.MiniPosts.ToListAsync();


        public async Task<int> GetCountAsync() => await _context.MiniPosts.CountAsync();

        public async Task<List<MiniPost>> GetPaginatedDatas(int page, int take)
        {
            return await _context.MiniPosts.Skip((page * take) - take).Take(take).ToListAsync();
        }
    }
}
