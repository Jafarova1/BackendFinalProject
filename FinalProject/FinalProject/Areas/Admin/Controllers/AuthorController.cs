using FinalProject.Areas.Admin.ViewModels.Author;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAuthorService _authorService;


        public AuthorController(AppDbContext context, IWebHostEnvironment env, IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _authorService= authorService;



        }
        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _authorService.GetAll();
            return View(authors);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Author author = await _authorService.GetById(id);
                if (author == null) return NotFound();
                return View(author);
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
        public async Task<IActionResult> Create(AuthorCreateVM author)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(author);
                }

                if (!author.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(author);
                }

                if (!author.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(author);

                }

                string fileName = Guid.NewGuid().ToString() + " " + author.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                await FileHelper.SaveFileAsync(newPath, author.Photo);

                Author newAuthor = new()
                {
                    Image = fileName,
                    FullName=author.FullName,
                    Description = author.Description

                };
                await _context.Authors.AddAsync(newAuthor);
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
                Author author = await _authorService.GetById(id);
                if (author == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", author.Image);
                FileHelper.DeleteFile(path);
                _context.Authors.Remove(author);
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
                Author dbAuthor = await _authorService.GetById(id);
                if (dbAuthor == null) return NotFound();

                AuthorEditVM model = new()
                {
                    Image = dbAuthor.Image,
                    Fullname = dbAuthor.FullName,
                    Description = dbAuthor.Description,
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
        public async Task<IActionResult> Edit(int? id, AuthorEditVM author)
        {
            try
            {


                if (id == null) return BadRequest();
                Author dbAuthor = await _authorService.GetById(id);
                if (dbAuthor == null) return NotFound();

                AuthorEditVM model = new()
                {
                    Image = dbAuthor.Image,
                    Fullname = dbAuthor.FullName,
                    Description = dbAuthor.Description,

                };
                if (author.Photo != null)
                {
                    if (!author.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!author.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbAuthor.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + author.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, author.Photo);
                    dbAuthor.Image = fileName;
                }
                else
                {
                    Author newAuthor = new()
                    {
                        Image = dbAuthor.Image
                    };
                }

                dbAuthor.Description = author.Description;
                dbAuthor.FullName = author.Fullname;


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
