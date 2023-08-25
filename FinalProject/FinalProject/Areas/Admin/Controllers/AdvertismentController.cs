using FinalProject.Areas.Admin.ViewModels.Advertisment;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertismentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAdvertismentService _advertismentService;
        public AdvertismentController(AppDbContext context,
                                     IWebHostEnvironment env,
                                     IAdvertismentService advertismentService)
        {
            _context = context;
            _env = env;
            _advertismentService = advertismentService;
        }
        public async Task<IActionResult> Index()
        {
            List<Advertisment> advertisments = await _advertismentService.GetAll();
            return View(advertisments);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Advertisment dbAdvertisment = await _advertismentService.GetAdvertisingById(id);
                if (dbAdvertisment == null) return NotFound();
                return View(dbAdvertisment);
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
        public async Task<IActionResult> Create(AdvertismentCreateVM advertising)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(advertising);
                }

                if (!advertising.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(advertising);
                }

                if (!advertising.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(advertising);

                }

                string fileName = Guid.NewGuid().ToString() + " " + advertising.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                await FileHelper.SaveFileAsync(newPath, advertising.Photo);

                Advertisment newAdvertising = new()
                {
                    Image = fileName,
                    Title = advertising.Title,
                };
                await _context.Advertisments.AddAsync(newAdvertising);
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
                Advertisment dbAdvertisment = await _advertismentService.GetAdvertisingById(id);
                if (dbAdvertisment == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", dbAdvertisment.Image);
                FileHelper.DeleteFile(path);
                _context.Advertisments.Remove(dbAdvertisment);
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
                Advertisment dbAdvertisment = await _advertismentService.GetAdvertisingById(id);
                if (dbAdvertisment == null) return NotFound();

                AdvertismentEditVM model = new()
                {
                    Image = dbAdvertisment.Image,
                    Title = dbAdvertisment.Title,
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
        public async Task<IActionResult> Edit(int? id, AdvertismentEditVM advertising)
        {
            try
            {
                if (id == null) return BadRequest();
                Advertisment dbAdvertisment = await _advertismentService.GetAdvertisingById(id);
                if (dbAdvertisment == null) return NotFound();

                AdvertismentEditVM model = new()
                {
                    Image = dbAdvertisment.Image,
                    Title = dbAdvertisment.Title,
                };
                if (advertising.Photo != null)
                {
                    if (!advertising.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!advertising.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbAdvertisment.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + advertising.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, advertising.Photo);
                    dbAdvertisment.Image = fileName;
                }
                else
                {
                    Advertisment newAdvertising = new()
                    {
                        Image = dbAdvertisment.Image
                    };
                }


                dbAdvertisment.Title = advertising.Title;

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
