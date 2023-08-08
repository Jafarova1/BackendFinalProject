using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Composition;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        public HomeController(AppDbContext context,IBlogService blogService)
        {
            _context = context;
            _blogService= blogService;
            
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();
            HomeVM model = new()
            {
                Blogs = blogs,

            };
            return View(model);
        }



    }
}