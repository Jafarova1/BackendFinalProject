using FinalProject.Areas.Admin.ViewModels.RecentBlog;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecentBlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IRecentBlogService _recentBlogService;


        public RecentBlogController(AppDbContext context, IWebHostEnvironment env,IRecentBlogService recentBlogService)
        {
            _context = context;
            _env = env;
            _recentBlogService = recentBlogService;
        }
        public async Task<IActionResult> Index()
        {
            List<RecentBlog> recentBlogs = await _recentBlogService.GetAll();
            return View(recentBlogs);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                RecentBlog recentBlog = await _recentBlogService.GetById(id);
                if (recentBlog == null) return NotFound();
                return View(recentBlog);
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
        public async Task<IActionResult> Create(RecentBlogCreateVM recentBlog)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(recentBlog);
                }

                if (!recentBlog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(recentBlog);
                }

                if (!recentBlog.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(recentBlog);

                }

                string fileName = Guid.NewGuid().ToString() + " " + recentBlog.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                await FileHelper.SaveFileAsync(newPath, recentBlog.Photo);

                RecentBlog newRecentBlog = new()
                {
                    Image = fileName,
                    Description = recentBlog.Description,

                };
                await _context.RecentBlogs.AddAsync(newRecentBlog);
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
                RecentBlog recentBlog = await _recentBlogService.GetById(id);
                if (recentBlog == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", recentBlog.Image);
                FileHelper.DeleteFile(path);
                _context.RecentBlogs.Remove(recentBlog);
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
                RecentBlog dbRecentBlog = await _recentBlogService.GetById(id);
                if (dbRecentBlog == null) return NotFound();

                RecentBlogEditVM model = new()
                {
                    Image = dbRecentBlog.Image,
                    Description = dbRecentBlog.Description,

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
        public async Task<IActionResult> Edit(int? id, RecentBlogEditVM recentBlog)
        {
            try
            {


                if (id == null) return BadRequest();
                RecentBlog dbRecentBlog = await _recentBlogService.GetById(id);
                if (dbRecentBlog == null) return NotFound();

                RecentBlogEditVM model = new()
                {
                    Image = dbRecentBlog.Image,
                    Description = dbRecentBlog.Description,

                };
                if (recentBlog.Photo != null)
                {
                    if (!recentBlog.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!recentBlog.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbRecentBlog.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + recentBlog.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, recentBlog.Photo);
                    dbRecentBlog.Image = fileName;
                }
                else
                {
                    RecentBlog newRecentBlog = new()
                    {
                        Image = dbRecentBlog.Image
                    };
                }

                dbRecentBlog.Description = recentBlog.Description;


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
