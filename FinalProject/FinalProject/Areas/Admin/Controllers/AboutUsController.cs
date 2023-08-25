
using FinalProject.Areas.Admin.ViewModels.About;
using FinalProject.Areas.Admin.ViewModels.AboutUs;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController:Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutUsService _aboutUsService;
        public AboutUsController(AppDbContext context, IWebHostEnvironment env, IAboutUsService aboutUsService)
        {
            _context = context;
            _env = env;
            _aboutUsService = aboutUsService;
        }

        public async Task<IActionResult> Index()
        {
            List<AboutUs> aboutUss = await _aboutUsService.GetAll();
            return View(aboutUss);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
                if (aboutUs == null) return NotFound();
                return View(aboutUs);
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
        public async Task<IActionResult> Create(AboutUsCreateVM aboutUs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(aboutUs);
                }

                if (!aboutUs.SmallPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutUs);
                }

                if (!aboutUs.SmallPhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutUs);

                }
                if (!aboutUs.LargePhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(aboutUs);
                }

                if (!aboutUs.LargePhoto.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(aboutUs);

                }

                string fileNameSmallPhoto = Guid.NewGuid().ToString() + " " + aboutUs.SmallPhoto.FileName;
                string newPathSmallPhoto = FileHelper.GetFilePath(_env.WebRootPath, "images", fileNameSmallPhoto);
                await FileHelper.SaveFileAsync(newPathSmallPhoto, aboutUs.SmallPhoto);
                string fileNameLargePhoto = Guid.NewGuid().ToString() + " " + aboutUs.LargePhoto.FileName;
                string newPathLargePhoto = FileHelper.GetFilePath(_env.WebRootPath, "images", fileNameLargePhoto);
                await FileHelper.SaveFileAsync(newPathLargePhoto, aboutUs.LargePhoto);

                AboutUs newAboutUs = new()
                {
                   FirstImage= fileNameSmallPhoto,
                   SecondtImage= fileNameLargePhoto,
                   FirstDescription=aboutUs.FirstDescription,
                   SecondDescription=aboutUs.SecondDescription,
                    Title = aboutUs.Title,
                   

                };
                await _context.AboutUss.AddAsync(newAboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
                if (aboutUs == null) return NotFound();

                string pathSmallImg = FileHelper.GetFilePath(_env.WebRootPath, "images", aboutUs.FirstImage);
                string pathLargeImg = FileHelper.GetFilePath(_env.WebRootPath, "images", aboutUs.SecondtImage);


                FileHelper.DeleteFile(pathSmallImg);
                FileHelper.DeleteFile(pathLargeImg);



                _context.AboutUss.Remove(aboutUs);
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
                AboutUs aboutUs = await _aboutUsService.GetAboutUsById(id);
                if (aboutUs == null) return NotFound();

                AboutUsUpdateVM model = new()
                {
                    FirstImage = aboutUs.FirstImage,
                    SecondImage = aboutUs.SecondtImage,
                    Title = aboutUs.Title,
                    FirstDescription = aboutUs.FirstDescription,
                    SecondDescription= aboutUs.SecondDescription,
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
        public async Task<IActionResult> Edit(int? id, AboutUsUpdateVM aboutUs)
        {
            try
            {
                if (id == null) return BadRequest();
                AboutUs dbaboutUs = await _aboutUsService.GetAboutUsById(id);
                if (dbaboutUs == null) return NotFound();

                AboutUsUpdateVM model = new()
                {
                    FirstImage = aboutUs.FirstImage,
                    SecondImage = aboutUs.SecondImage,
                    Title = aboutUs.Title,
                    FirstDescription = aboutUs.FirstDescription,
                    SecondDescription = aboutUs.SecondDescription,
                };
                if (aboutUs.SmallPhoto != null)
                {
                    if (!aboutUs.SmallPhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }

                    if (!aboutUs.SmallPhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbaboutUs.FirstImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + aboutUs.SmallPhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, aboutUs.SmallPhoto);
                    dbaboutUs.FirstImage = fileName;
                }

                else
                {
                    AboutUs newAboutUs = new()
                    {
                        FirstImage = dbaboutUs.FirstImage
                    };
                }
                if (aboutUs.LargePhoto != null)
                {
                    if (!aboutUs.LargePhoto.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }

                    if (!aboutUs.LargePhoto.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);

                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "images", dbaboutUs.SecondtImage);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + aboutUs.LargePhoto.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);
                    await FileHelper.SaveFileAsync(newPath, aboutUs.LargePhoto);
                    dbaboutUs.SecondtImage = fileName;
                }
                else
                {
                    AboutUs newAboutUs = new()
                    {
                        SecondtImage = dbaboutUs.SecondtImage
                    };
                }
                dbaboutUs.Title = aboutUs.Title;
                dbaboutUs.FirstDescription = aboutUs.FirstDescription;
                dbaboutUs.SecondDescription = aboutUs.SecondDescription;
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
