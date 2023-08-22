using FinalProject.Areas.Admin.ViewModels.Post;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMiniPostService _miniPostService;
  

        public PostController(AppDbContext context, IWebHostEnvironment env, IMiniPostService miniPostService)
        {
            _context = context;
            _env = env;
            _miniPostService = miniPostService;

            
            
        }
        public async Task<IActionResult> Index()
        {
            List<MiniPost> posts = await _miniPostService.GetAll();
            return View(posts);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                MiniPost post = await _miniPostService.GetById(id);
                if (post == null) return NotFound();
                return View(post);
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
        public async Task<IActionResult> Create(PostCreateVM post)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(post);
                }

                if (!post.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(post);
                }

                if (!post.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(post);

                }

                string fileName = Guid.NewGuid().ToString() + " " + post.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                await FileHelper.SaveFileAsync(newPath, post.Photo);

                MiniPost newPost = new()
                {
                    Image = fileName,
                    Title = post.Title,
                    Description=post.Description

                };
                await _context.MiniPosts.AddAsync(newPost);
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
                MiniPost post = await _miniPostService.GetById(id);
                if (post == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "/images", post.Image);
                FileHelper.DeleteFile(path);
                _context.MiniPosts.Remove(post);
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
                MiniPost dbPost = await _miniPostService.GetById(id);
                if (dbPost == null) return NotFound();

                PostEditVM model = new()
                {
                    Image = dbPost.Image,
                    Title = dbPost.Title,
                    Description = dbPost.Description,
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
        public async Task<IActionResult> Edit(int? id, PostEditVM post)
        {
            try
            {


                if (id == null) return BadRequest();
                MiniPost dbPost = await _miniPostService.GetById(id);
                if (dbPost == null) return NotFound();

                PostEditVM model = new()
                {
                    Image = dbPost.Image,
                    Title = dbPost.Title,
                    Description = dbPost.Description,

                };
                if (post.Photo != null)
                {
                    if (!post.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!post.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "/images", dbPost.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + post.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, post.Photo);
                    dbPost.Image = fileName;
                }
                else
                {
                    MiniPost newPost = new()
                    {
                        Image = dbPost.Image
                    };
                }

                dbPost.Title = post.Title;
                dbPost.Description = post.Description;


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
