using FinalProject.Areas.Admin.ViewModels.DessertMenu;
using FinalProject.Areas.Admin.ViewModels.StarterMenu;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Areas.Admin.Controllers
{
    public class DesserMenuController : Controller
    {
        private readonly IStarterMenuService _starterMenuService;
        private readonly IDessertMenuService _dessertMenuService;
        private readonly ICategoryService _categoryService;
        private readonly ISettingService _settingService;



        public DesserMenuController(IStarterMenuService starterMenuService,
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
            var paginatedDatas = await _dessertMenuService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<DessertMenuVM> mappedDatas = _dessertMenuService.GetMappedDatas(paginatedDatas);

            PaginateData<DessertMenuVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _dessertMenuService.GetCountAsync();

            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Dessert product = await _dessertMenuService.GetWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            return View(_dessertMenuService.GetMappedData(product));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCategoriesAndDiscounts();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DessertMenuCreateVM request)
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

            await _dessertMenuService.CreateAsync(request);
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

            var product = await _dessertMenuService.GetWithIncludesAsync((int)id);

            if (product is null) return NotFound();

            await GetCategoriesAndDiscounts();

            DessertMenuEditVM response = new()
            {
                Name = product.Title,
                Description = product.Description,
                Price = product.Price.ToString("0.####").Replace(",", "."),
                Images=product.Images.ToList(),

            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DessertMenuEditVM request)
        {
            await GetCategoriesAndDiscounts();

            var product = await _dessertMenuService.GetWithIncludesAsync((int)id);

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

            await _dessertMenuService.EditAsync(id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            await _dessertMenuService.DeleteImageByIdAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _dessertMenuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
