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
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public async Task CreateAsync(List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "img");

                Blog blog = new()
                {
                    Image = fileName
                };

                await _context.Blogs.AddAsync(blog);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await GetByIdAsync(id);

            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", blog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(Blog blog, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "img", blog.Image);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "img");

            blog.Image = fileName;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<List<BlogVM>> GetAllMappedDatasAsync()
        {
            List<BlogVM> blogList = new();

            List<Blog> blogs = await GetAllAsync();

            foreach (Blog blog in blogs)
            {
                BlogVM model = new()
                {
                    Id = blog.Id,
                    //Image = blog.Image,
                    //CreateDate = blog.CreatedDate.ToString("dd-MM-yyyy")
                };

                blogList.Add(model);
            }

            return blogList;
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

