using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class StoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IStoryService _storyService;

        public StoriesController(AppDbContext context,ILayoutService layoutService,IStoryService storyService)
        {
            _context = context;
            _layoutService = layoutService;
            _storyService = storyService;
                
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            List<Story> stories = await _storyService.GetAll();
            List<Story> paginateStories = await _storyService.GetPaginatedDatas(page, take);
            int pageCount = await GetPageCountAsync(take);
            PaginateData<Story> paginatedDatas = new(paginateStories, page, pageCount);
            StoryVM model = new()
            {
                Stories = stories,
                PaginatedDatas = paginatedDatas

            };
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var storyCount = await _storyService.GetCountAsync();

            return (int)Math.Ceiling((decimal)storyCount / take);
        }
        //[HttpGet]
        //public async Task<IActionResult> StoryDetail(int? id)
        //{
        //    if (id is null) return BadRequest();
        //    //var dbStory = await _storyService.GetById((int)id);
        //    if (dbStory is null) return NotFound();
        //    var stories = await _storyService.GetAll();

        //    StoryVM model = new()
        //    {
        //        //Story = dbStory,
        //        Stories = stories.ToList(),
        //    };
        //    return View(model);
        //}

    }
}
