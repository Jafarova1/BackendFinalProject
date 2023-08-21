using FinalProject.Areas.Admin.ViewModels.StarterMenu;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Areas.Admin.Controllers
{
    public class StarterMenuController : Controller
    {
        private readonly IStarterMenuService _starterMenuService;
        private readonly IDessertMenuService _dessertMenuService;
        private readonly ICategoryService _categoryService;
        private readonly ISettingService _settingService;



        public StarterMenuController(IStarterMenuService starterMenuService,
                                 IDessertMenuService dessertMenuService,
                                        ICategoryService categoryService,
                                        ISettingService settingService)
        {
            _starterMenuService = starterMenuService;
            _dessertMenuService = dessertMenuService;
            _categoryService = categoryService;
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var settingDatas = _settingService.GetAll();

            int take = int.Parse(settingDatas["AdminProductPaginateTake"]);
            var paginatedDatas = await _starterMenuService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<StarterMenuVM> mappedDatas = _starterMenuService.GetMappedDatas(paginatedDatas);

            PaginateData<StarterMenuVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _starterMenuService.GetCountAsync();

            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Starter product = await _starterMenuService.GetWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            return View(_starterMenuService.GetMappedData(product));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCategoriesAndDiscounts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StarterMenuCreateVM request)
        {
            await GetCategoriesAndDiscounts();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }


                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }
            }

            await _starterMenuService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        private async Task GetCategoriesAndDiscounts()
        {
            ViewBag.categories = await GetCategories();
         
        }


        private async Task<SelectList> GetCategories()
        {
            List<Category> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _starterMenuService.GetWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            await GetCategoriesAndDiscounts();

            StarterMenuEditVM response = new()
            {
                Name = product.Title,
                Description = product.Description,
                Price = product.Price.ToString("0.####").Replace(",", "."),
                Images = product.Images.ToList()
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StarterMenuEditVM request)
        {
            await GetCategoriesAndDiscounts();

            var product = await _starterMenuService.GetWithIncludesAsync((int)id);

            if (!ModelState.IsValid)
            {
                request.Images = product.Images.ToList();
                return View(request);
            }

            if (request.NewImages != null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Image", "Please select only image file");
                        request.Images = product.Images.ToList();
                        return View(request);
                    }


                    if (item.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Image", "Image size must be max 200 KB");
                        request.Images = product.Images.ToList();
                        return View(request);
                    }
                }
            }

            await _starterMenuService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            await _starterMenuService.DeleteImageByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _starterMenuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
