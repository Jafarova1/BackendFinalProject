
using FinalProject.Areas.Admin.ViewModels.Story;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class StoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IStoryService _storyService;
        private readonly IAuthorService _authorService;

        public StoriesController(AppDbContext context, IWebHostEnvironment env, IBlogService blogService, IAuthorService authorService,IStoryService storyService)
        {
            _context = context;
            _env = env;
            _authorService = authorService;
            _storyService = storyService;
        }
        public async Task<IActionResult> Index()
        {
            List<Story> stories = await _storyService.GetAll();
            return View(stories);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Story story = await _storyService.GetById(id);
                if (story == null) return NotFound();
                return View(story);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoryCreateVM story)
        {
            try
            {
                

                if (!ModelState.IsValid)
                {
                    return View(story);
                }

                if (!story.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(story);
                }

                if (!story.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(story);

                }

                string fileName = Guid.NewGuid().ToString() + " " + story.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                await FileHelper.SaveFileAsync(newPath, story.Photo);

                Story newStory = new()
                {
                    Image = fileName,
                    Title = story.Title,

                };
                await _context.Stories.AddAsync(newStory);
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
                Story story = await _storyService.GetById(id);
                if (story == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", story.Image);
                FileHelper.DeleteFile(path);
                _context.Stories.Remove(story);
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
                Story dbStory = await _storyService.GetById(id);
                if (dbStory == null) return NotFound();

                StoryEditVM model = new()
                {
                    Image = dbStory.Image,
                    Title = dbStory.Title,
    
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
        public async Task<IActionResult> Edit(int? id, StoryEditVM story)
        {
            try
            {
            

                if (id == null) return BadRequest();
                Story dbStory = await _storyService.GetById(id);
                if (dbStory == null) return NotFound();

                StoryEditVM model = new()
                {
                    Image = dbStory.Image,
                    Title = dbStory.Title,

                };
                if (story.Photo != null)
                {
                    if (!story.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!story.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbStory.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + story.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, story.Photo);
                    dbStory.Image = fileName;
                }
                else
                {
                    Story newStory = new()
                    {
                        Image = dbStory.Image
                    };
                }

                dbStory.Title = story.Title;
        

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
