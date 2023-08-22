using FinalProject.Areas.Admin.ViewModels.Category;
using FinalProject.Areas.Admin.ViewModels.Social;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context, IWebHostEnvironment env, ICategoryService categoryService)
        {
            _context = context;
            _env = env;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _categoryService.GetAll();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Category category = await _categoryService.GetCategoryById(id);
                if (category == null) return NotFound();
                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                Category newCategory = new()
                {
                    Name=category.Name,
                    Number=category.Number,
                };
                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();

                _context.Categories.Remove(dbCategory);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();

                CategoryEditVM model = new()
                {
                    Name=dbCategory.Name,
                    Number=dbCategory.Number,

                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM category)
        {
            try
            {
                if (id == null) return BadRequest();
                Category dbCategory = await _categoryService.GetCategoryById(id);
                if (dbCategory == null) return NotFound();

                dbCategory.Name = category.Name;
                dbCategory.Number = category.Number;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
