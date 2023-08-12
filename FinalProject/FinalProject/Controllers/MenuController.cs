using FinalProject.Data;
using FinalProject.Models;
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
        public MenuController(AppDbContext context,ILayoutService layoutService,IStarterMenuService starterMenuService,IDessertMenuService dessertMenuService)
        {
                _context = context;
            _layoutService = layoutService;
            _dessertMenuService = dessertMenuService;
            _starterMenuService = starterMenuService;
        }
    //    private static List<MenuItem> menuItems = new List<MenuItem>
    //{
    //    new MenuItem { Id = 1, Name = "Burger", Price = 10.99m, Description = "Delicious beef burger with lettuce and tomato" },
    //    new MenuItem { Id = 2, Name = "Pizza", Price = 14.99m, Description = "Pepperoni and cheese pizza" }
    //};

        public async IActionResult Index()
        {
            Starter starter = await _context.Starters.FirstOrDefaultAsync();
            Dessert dessert=await _context.Desserts.FirstOrDefaultAsync();
            List<StarterMenuImage> starterMenuImages = await _starterMenuService.GetAll();
            FoodVM food = new()
            {

            };
            return await _starterMenuService.GetAll();
     
        }

        [HttpPost]
        //public IActionResult Order(int itemId)
        //{
        //    var selectedItem = menuItems.Find(item => item.Id == itemId);
        //    if (selectedItem != null)
        //    {
        //        // Process the order or take any necessary actions
        //        return Content($"You've ordered {selectedItem.Name}");
        //    }

        //    return NotFound();
        //}
    }
}
