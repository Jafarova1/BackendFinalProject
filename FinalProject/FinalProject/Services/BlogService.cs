using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAll() => await _context.Blogs.ToListAsync();

        public async Task<Blog> GetById(int? id) => await _context.Blogs.Include(m => m.Image)
                                                                         .Include(m => m.Title)
                                                                         .FirstOrDefaultAsync(m => m.Id == id);
    }
}

