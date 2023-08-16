using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IStarterMenuService _starterMenuService;
        private readonly IDessertMenuService _dessertMenuService;
        private readonly IOrderService _orderService;
        public MenuController(AppDbContext context,ILayoutService layoutService,IStarterMenuService starterMenuService,IDessertMenuService dessertMenuService,IOrderService orderService)
        {
                _context = context;
            _layoutService = layoutService;
            _dessertMenuService = dessertMenuService;
            _starterMenuService = starterMenuService;
            _orderService = orderService;
        }


        public  async Task<IActionResult> Index()
        {
            //IEnumerable<StarterMenuImage> starterMenuImages = await _context.StarterMenuImages.Where(m => !m.SoftDelete).ToListAsync();
            //IEnumerable<DessertMenuImage> dessertMenuImages = await _context.DessertMenuImages.Where(m => !m.SoftDelete).ToListAsync();

            //IEnumerable<Starter> starters = await _starterMenuService.GetAll();
            //IEnumerable<Dessert> desserts = await _dessertMenuService.GetAll();
            //FoodVM model = new()
            //{
            //    StarterMenuImages = starterMenuImages.ToList(),
            //    DessertMenuImages = dessertMenuImages.ToList(),
            //    Starters = starters.ToList(),
            //    Desserts = desserts.ToList()



            //};

     
            Order orders = await _context.Orders.FirstOrDefaultAsync();
            List<Starter> starters = await _context.Starters.Include(m => m.Images).Take(5).ToListAsync(); 
            List<Dessert> desserts = await _context.Desserts.Include(m => m.Images).Take(5).ToListAsync();

            //List<AboutSlider> aboutSliders = await _aboutSliderService.GetAll();
            FoodVM model = new()
            {
                //AboutUs = aboutUs,
                //AboutSliders = aboutSliders
                Starter=starters,
                Dessert=desserts,
                Order=orders
                
                

            };
            return View(model);



            //return View(model);
     
        }


    }
}
