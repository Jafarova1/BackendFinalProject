using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO;

namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly IAboutSliderService _aboutSliderService;
        private readonly IAboutUsService _aboutUsService;
        private readonly ISubscribeService _subscribeService;
        public AboutController(AppDbContext context,LayoutService layoutService,IAboutSliderService aboutSliderService,IAboutUsService aboutUsService,ISubscribeService subscribeService)
        {
                _context = context;
            _layoutService = layoutService;
            _aboutSliderService = aboutSliderService;
            _aboutUsService = aboutUsService;
            _subscribeService = subscribeService;
        }
        public async Task<IActionResult> Index()
        {
            AboutUs aboutUs = await _context.AboutUss.FirstOrDefaultAsync();
            List<AboutSlider> aboutSliders = await _aboutSliderService.GetAll();
            AboutVM model = new()
            {
                AboutUs = aboutUs,
                AboutSliders= aboutSliders
    
            };
            return View(model);
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
