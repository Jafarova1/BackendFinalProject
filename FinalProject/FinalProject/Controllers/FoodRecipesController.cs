using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels.FoodRecipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class FoodRecipesController:Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly IFoodRecipeService _foodRecipeService;
        private readonly ICategoryService _categoryService;
        public FoodRecipesController(AppDbContext context,LayoutService layoutService,IFoodRecipeService foodRecipeService,ICategoryService categoryService)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(FoodCommentVM model)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", model);
                var existEmail = await _context.FoodComments.FirstOrDefaultAsync(m => m.Email == model.Email);

                if (existEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return RedirectToAction("Index");
                }
                FoodComment comment = new()
                {
                    Email = model.Email,
                    Message=model.Message,
                    Name= model.Name,   
                    
                   
                };

                await _context.FoodComments.AddAsync(comment);
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
