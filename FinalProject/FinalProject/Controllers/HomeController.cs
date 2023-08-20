using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using FinalProject.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Diagnostics;
using SubscribeVM = FinalProject.ViewModels.Home.SubscribeVM;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutSliderService _aboutSliderService;
        private readonly IAboutUsService _aboutUsService;
        private readonly ISubscribeService _subscribeService;

        public HomeController(AppDbContext context,IBlogService blogService,IAboutSliderService aboutSliderService,IAboutUsService aboutUsService,ISubscribeService subscribeService)
        {
            _context = context;
            _aboutSliderService = aboutSliderService;
            _aboutUsService= aboutUsService;
            _subscribeService=subscribeService;
       
            
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.ToListAsync();
            IEnumerable<Post> posts = await _context.Posts.ToListAsync();
            IEnumerable<Recipe> recipes = await _context.Recipes.ToListAsync();
            List<AboutSlider> aboutSliders = await _aboutSliderService.GetAll();
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            AboutUs aboutUs = await _context.AboutUss.FirstOrDefaultAsync();
            HomeVM homeModel = new()
            {
                Blogs = blogs,
                Posts = posts,
                Recipes = recipes,
                AboutSliders = aboutSliders,
                AboutUs=aboutUs,
                Advertisments=advertisments

            };
            return View(homeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSubscribe(SubscribeVM model)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", model);
                var existEmail = await _context.Subscribers.FirstOrDefaultAsync(m => m.Email == model.Email);

                if (existEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return RedirectToAction("Index");
                }
                Subscriber subscribe = new()
                {
                    Email = model.Email,
                };

                await _context.Subscribers.AddAsync(subscribe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index");
            }
        }



    }
}