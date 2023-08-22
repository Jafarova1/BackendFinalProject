using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class StoryService : IStoryService
    {
        private readonly AppDbContext _context;

        public StoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Story>> GetAll() => await _context.Stories.ToListAsync();/*Include(m => m.Image)*/

        public async Task<Story> GetById(int? id) => await _context.Stories.FirstOrDefaultAsync(m => m.Id == id);





        public async Task<int> GetCountAsync() => await _context.Stories.CountAsync();

        public async Task<List<Story>> GetPaginatedDatas(int page, int take)
        {
            return await _context.Stories.Skip((page * take) - take).Take(take).ToListAsync();

        }

    
    }
}
