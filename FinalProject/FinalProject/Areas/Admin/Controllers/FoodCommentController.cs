using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class FoodCommentController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IStarterMenuService _starterMenuService;
        private readonly IDessertMenuService _dessertMenuService;
        public FoodCommentController(AppDbContext context,IStarterMenuService starterMenuService,IDessertMenuService dessertMenuService)
        {
                _context = context;
           _starterMenuService = starterMenuService;    
            _dessertMenuService = dessertMenuService;
        }

        public async Task<IActionResult> Index()
        {
            var starterMenuComment = await _starterMenuService.GetComments();
            return View(starterMenuComment);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            FoodComment dbcomment = await _starterMenuService.GetCommentById((int)id);
            if (dbcomment is null) return NotFound();

            _context.FoodComments.Remove(dbcomment);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            FoodComment dbcomment = await _starterMenuService.GetCommentByIdWithProduct((int)id);
            if (dbcomment is null) return NotFound();
            return View(dbcomment);
        }
    }
}
