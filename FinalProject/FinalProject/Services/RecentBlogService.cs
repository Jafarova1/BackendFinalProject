using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class RecentBlogService : IRecentBlogService
    {
        private readonly AppDbContext _context;

        public RecentBlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<RecentBlog>> GetAll() => await _context.RecentBlogs.ToListAsync();

        public async Task<RecentBlog> GetById(int? id) => await _context.RecentBlogs.FirstOrDefaultAsync(m => m.Id == id);
    }
}
