using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class RecentBlogService : IRecentBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public RecentBlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //public async Task DeleteAsync(int id)
        //{
        //    RecentBlog recentBlog = await GetById(id);

        //    _context.RecentBlogs.Remove(recentBlog);

        //    await _context.SaveChangesAsync();

        //    string path = Path.Combine(_env.WebRootPath, "img", recentBlog.Image);

        //    if (File.Exists(path))
        //    {
        //        File.Delete(path);
        //    }
        //}

        public async Task<List<RecentBlog>> GetAll() => await _context.RecentBlogs.ToListAsync();

        public async Task<RecentBlog> GetById(int? id) => await _context.RecentBlogs.FirstOrDefaultAsync(m => m.Id == id);
    }
}
