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
        private readonly IAboutUsService _aboutUsService;
        public AboutController(AppDbContext context,ILayoutService layoutService,IAboutSliderService aboutSliderService,IAboutUsService aboutUsService)
        {
                _context = context;
            _layoutService = layoutService;
            _aboutSliderService = aboutSliderService;
            _aboutUsService = aboutUsService;
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
    }
}
