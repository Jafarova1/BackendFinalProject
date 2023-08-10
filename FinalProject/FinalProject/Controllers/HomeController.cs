using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
 
        public HomeController(AppDbContext context,IBlogService blogService)
        {
            _context = context;
       
            
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();
            IEnumerable<Post> posts = await _context.Posts.Where(m => !m.SoftDelete).ToListAsync();
            IEnumerable<Recipe> recipes = await _context.Recipes.Where(m => !m.SoftDelete).ToListAsync();
            HomeVM model = new()
            {
                Blogs = blogs,
                Posts = posts,
                Recipes = recipes

                

            };
            return View(model);
        }



    }
}