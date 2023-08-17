using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels.FoodRecipe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class FoodRecipesController:Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IFoodRecipeService _foodRecipeService;
        private readonly ICategoryService _categoryService;
        public FoodRecipesController(AppDbContext context,ILayoutService layoutService,IFoodRecipeService foodRecipeService,ICategoryService categoryService)
        {
            _context = context;
            _layoutService = layoutService;
            _foodRecipeService = foodRecipeService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryService.GetAll();
            List<RecentBlog> recentBlogs = await _context.RecentBlogs.ToListAsync();
            List<News> newss = await _context.Newss.ToListAsync();
            FoodRecipeVM model = new()
            {
                Categories=categories,
                RecentBlogs=recentBlogs,
                Newss=newss
            };


            return View(model);
        }
    }
}
