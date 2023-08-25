using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class AdditionalFoodController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly IAdditionalFoodService _additionalFoodService;
        public AdditionalFoodController(AppDbContext context, LayoutService layoutService, IAdditionalFoodService additionalFoodService)
        {
            _context = context;
            _layoutService = layoutService;
            _additionalFoodService = additionalFoodService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            //List<Dessert> desserts = await _context.Desserts.Include(m => m.Images).Take(5).ToListAsync();
            List<AdditionalFood> additionalFoods = await _additionalFoodService.GetAll();
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            List<AdditionalFood> paginateFoods = await _additionalFoodService.GetPaginatedDatas(page, take);
            int pageCount = await GetPageCountAsync(take);
            PaginateData<AdditionalFood> paginatedDatas = new(paginateFoods, page, pageCount);
            AdditionalFoodVM model = new()
            {
            
                AdditionalFoods = additionalFoods,
                Advertisments = advertisments,
                AddFoods= paginatedDatas



            };
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var storyCount = await _additionalFoodService.GetCountAsync();

            return (int)Math.Ceiling((decimal)storyCount / take);
        }
    }
}
