using FinalProject.Areas.Admin.ViewModels.AdditionalFood;

using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class AdditionFoodController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAdditionalFoodService _additionalFoodService;


        public AdditionFoodController(AppDbContext context, IWebHostEnvironment env, IAdditionalFoodService additionalFoodService)
        {
            _context = context;
            _env = env;
            _additionalFoodService = additionalFoodService;



        }
        public async Task<IActionResult> Index()
        {
            List<AdditionalFood> additionalFoods = await _additionalFoodService.GetAll();
            return View(additionalFoods);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AdditionalFood additionalFood = await _additionalFoodService.GetById(id);
                if (additionalFood == null) return NotFound();
                return View(additionalFood);
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
        public async Task<IActionResult> Create(AdditionalFoodCreateVM addFood)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(addFood);
                }

                if (!addFood.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(addFood);
                }

                if (!addFood.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(addFood);

                }

                string fileName = Guid.NewGuid().ToString() + " " + addFood.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                await FileHelper.SaveFileAsync(newPath, addFood.Photo);

                AdditionalFood newAddFood = new()
                {
                    Image = fileName,
                    Title = addFood.Title,
                    Description = addFood.Description

                };
                await _context.AdditionalFoods.AddAsync(newAddFood);
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
                AdditionalFood additionalFood = await _additionalFoodService.GetById(id);
                if (additionalFood == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "/images", additionalFood.Image);
                FileHelper.DeleteFile(path);
                _context.AdditionalFoods.Remove(additionalFood);
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
                AdditionalFood dbAdditionalFood = await _additionalFoodService.GetById(id);
                if (dbAdditionalFood == null) return NotFound();

                AdditionalFoodEditVM model = new()
                {
                    Image = dbAdditionalFood.Image,
                    Title = dbAdditionalFood.Title,
                    Description = dbAdditionalFood.Description,
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
        public async Task<IActionResult> Edit(int? id, AdditionalFoodEditVM addFood)
        {
            try
            {


                if (id == null) return BadRequest();
                AdditionalFood dbAdditionalFood = await _additionalFoodService.GetById(id);
                if (dbAdditionalFood == null) return NotFound();

                AdditionalFoodEditVM model = new()
                {
                    Image = dbAdditionalFood.Image,
                    Title = dbAdditionalFood.Title,
                    Description= dbAdditionalFood.Description,

                };
                if (addFood.Photo != null)
                {
                    if (!addFood.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!addFood.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "/images", dbAdditionalFood.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + addFood.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, addFood.Photo);
                    dbAdditionalFood.Image = fileName;
                }
                else
                {
                    AdditionalFood newAddFood = new()
                    {
                        Image = addFood.Image
                    };
                }

                dbAdditionalFood.Title = addFood.Title;


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
