using FinalProject.Areas.Admin.ViewModels.About;
using FinalProject.Areas.Admin.ViewModels.AboutSlider;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class AboutSliderController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly AboutSliderService _aboutSliderService;

        public AboutSliderController(AppDbContext context,
                                IWebHostEnvironment env,
                                AboutSliderService aboutSliderService)
        {
            _context = context;
            _env = env;
            _aboutSliderService =aboutSliderService ;
        }
        public async Task<IActionResult> Index()
        {
            List<AboutSlider> sliders = await _aboutSliderService.GetAll();
            return View(sliders);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutSlider slider = await _aboutSliderService.GetAboutSliderById(id);

                if (slider == null) return NotFound();
                return View(slider);
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
        public async Task<IActionResult> Create(AboutSliderCreateVM slider)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(slider);
                }

                if (!slider.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(slider);
                }

                if (!slider.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(slider);

                }

                string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                await FileHelper.SaveFileAsync(newPath, slider.Photo);

                AboutSlider newSlider = new()
                {
                    Image = fileName,
                    Title = slider.Title,
                    Description = slider.ShortDesc,

                };
                await _context.AboutSliders.AddAsync(newSlider);
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
                AboutSlider slider = await _aboutSliderService.GetAboutSliderById(id);
                if (slider == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", slider.Image);
                FileHelper.DeleteFile(path);
                _context.AboutSliders.Remove(slider);
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
                AboutSlider dbSlider = await _aboutSliderService.GetAboutSliderById(id);
                if (dbSlider == null) return NotFound();

                AboutSliderUpdateVM model = new()
                {
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    ShortDesc = dbSlider.Description,
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
        public async Task<IActionResult> Edit(int? id, AboutSliderUpdateVM slider)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutSlider dbSlider = await _aboutSliderService.GetAboutSliderById(id);
                if (dbSlider == null) return NotFound();

                AboutSliderUpdateVM model = new()
                {
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    ShortDesc = dbSlider.Description,

                };
                if (slider.Photo != null)
                {
                    if (!slider.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!slider.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", dbSlider.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + slider.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, slider.Photo);
                    dbSlider.Image = fileName;
                }
                else
                {
                    AboutSlider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }

                dbSlider.Title = slider.Title;
                dbSlider.Description = slider.ShortDesc;


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
