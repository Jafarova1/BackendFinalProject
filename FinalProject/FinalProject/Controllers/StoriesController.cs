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
    public class StoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly IStoryService _storyService;

        public StoriesController(AppDbContext context,LayoutService layoutService,IStoryService storyService)
        {
            _context = context;
            _layoutService = layoutService;
            _storyService = storyService;
                
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
         
            List<Story> stories = await _storyService.GetAll();
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            List<Story> paginateStories = await _storyService.GetPaginatedDatas(page, take);
            int pageCount = await GetPageCountAsync(take);
            PaginateData<Story> paginatedDatas = new(paginateStories, page, pageCount);
            StoryVM model = new()
            {
                Stories = stories,
                PaginatedDatas = paginatedDatas,
                Advertisments= advertisments


            };
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var storyCount = await _storyService.GetCountAsync();

            return (int)Math.Ceiling((decimal)storyCount / take);
        }


    }
}
