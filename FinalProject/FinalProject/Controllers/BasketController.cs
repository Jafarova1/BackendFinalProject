using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModels;
using FinalProject.ViewModels.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProject.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public BasketController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            var basket = await _context.Baskets
         .Include(m => m.BasketProducts)
         .ThenInclude(m => m.Starter)
         .Include(m => m.BasketProducts)
         .ThenInclude(m => m.Dessert)
         .ThenInclude(m => m.Images)
         .FirstOrDefaultAsync(m => m.AppUserId == user.Id);


            BasketIndexVM model = new BasketIndexVM();

            if (basket == null) return View(model);
            foreach (var dbBasketProduct in basket.BasketProducts)
            {
                BasketProductVM basketProduct = new BasketProductVM
                {
                    Id = dbBasketProduct.Id,
                    StarterMenuId = dbBasketProduct.StarterId,
                    DessertMenuId = dbBasketProduct.DessertId,
                    //StarterImage = dbBasketProduct.Starter.Images.FirstOrDefault(m=>m.IsMain).Image,
                    DessertImage = dbBasketProduct.Dessert.Images.FirstOrDefault(m => m.IsMain).Image,
                    StarterMenuPrice = dbBasketProduct.Starter.Price,
                    DessertMenuPrice = dbBasketProduct.Dessert.Price,
                    Quantity = dbBasketProduct.Quantity,
                    DessertTotal = (dbBasketProduct.Dessert.Price * dbBasketProduct.Quantity),

                };

                model.BasketProducts.Add(basketProduct);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBasket(BasketAddVM basketAddVM)
        {
            if (!ModelState.IsValid) return BadRequest(basketAddVM);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var starter = await _context.Starters.FindAsync(basketAddVM.Id);
            if (starter == null) return NotFound();

            var dessert = await _context.Desserts.FindAsync(basketAddVM.Id);
            if (dessert == null) return NotFound();

            var basket = await _context.Baskets.FirstOrDefaultAsync(m => m.AppUserId == user.Id);
            if (basket == null)
            {
                basket = new Basket
                {
                    AppUserId = user.Id
                };

                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }

            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(bp => bp.StarterId == starter.Id && bp.BasketId == basket.Id
                && bp.DessertId==dessert.Id );

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
            }
            else
            {
                basketProduct = new BasketProduct
                {
                    BasketId = basket.Id,
                    StarterId = starter.Id,
                    DessertId = dessert.Id,
                    Quantity = 1
                };
                await _context.BasketProducts.AddAsync(basketProduct);

            }

            await _context.SaveChangesAsync();
            return Ok();
        }
                [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(bp => bp.Id == id
                && bp.Basket.AppUserId == user.Id);

            if (basketProduct == null) return NotFound();

            var starter = await _context.Starters.FirstOrDefaultAsync(p => p.Id == basketProduct.StarterId);
  
            if (starter == null) return NotFound();

            var dessert = await _context.Desserts.FirstOrDefaultAsync(p => p.Id == basketProduct.DessertId);

            if (dessert == null) return NotFound();

            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }

}
