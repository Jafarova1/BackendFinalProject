using FinalProject.Data;
using FinalProject.Models;
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
        private readonly ILayoutService _layoutService;
        private readonly IAboutSliderService _aboutSliderService;
        public AboutController(AppDbContext context,ILayoutService layoutService,IAboutSliderService aboutSliderService)
        {
                _context = context;
            _layoutService = layoutService;
            _aboutSliderService = aboutSliderService;
        }
        public async Task<IActionResult> Index()
        {
            AboutBanner aboutBanner = await _context.AboutBanners.FirstOrDefaultAsync();
            List<AboutSlider> aboutSliders = await _aboutSliderService.GetAll();
            AboutVM model = new()
            {
                AboutBanner = aboutBanner,
                AboutSliders= aboutSliders
    
            };
            return View(model);
        }
    }
}
