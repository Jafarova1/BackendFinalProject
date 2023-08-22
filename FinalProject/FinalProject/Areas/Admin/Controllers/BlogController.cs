using FinalProject.Areas.Admin.ViewModels.Advertisment;
using FinalProject.Areas.Admin.ViewModels.Blog;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        public BlogController(AppDbContext context,
                                     IWebHostEnvironment env,
                                     IBlogService blogService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAll();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog dbblog = await _blogService.GetById(id);
                if (dbblog == null) return NotFound();
                return View(dbblog);
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
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(blog);
                }

                if (!blog.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(blog);
                }

                if (!blog.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(blog);

                }

                string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                await FileHelper.SaveFileAsync(newPath, blog.Photo);

                Blog newBlog = new()
                {
                    Image = fileName,
                    Title = blog.Title,
                };
                await _context.Blogs.AddAsync(newBlog);
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
                Blog dbBlog = await _blogService.GetById(id);
                if (dbBlog == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "/images", dbBlog.Image);
                FileHelper.DeleteFile(path);
                _context.Blogs.Remove(dbBlog);
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
                Blog dbBlog = await _blogService.GetById(id);
                if (dbBlog == null) return NotFound();

                BlogEditVM model = new()
                {
                    Image = dbBlog.Image,
                    Title = dbBlog.Title,
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
        public async Task<IActionResult> Edit(int? id, BlogEditVM blog)
        {
            try
            {
                if (id == null) return BadRequest();
                Blog dbBlog = await _blogService.GetById(id);
                if (dbBlog == null) return NotFound();

                BlogEditVM model = new()
                {
                    Image = dbBlog.Image,
                    Title = dbBlog.Title,
                };
                if (blog.Photo != null)
                {
                    if (!blog.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!blog.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "/images", dbBlog.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + blog.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, blog.Photo);
                    dbBlog.Image = fileName;
                }
                else
                {
                    Blog newBlog = new()
                    {
                        Image = dbBlog.Image
                    };
                }


                dbBlog.Title = blog.Title;

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
