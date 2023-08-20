using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProject.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public CartService(IHttpContextAccessor httpContextAccessor,
                            AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public void DeleteData(int? id)
        {
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedStarterFood = baskets.FirstOrDefault(b => b.StarterMenuId == id);
            var deletedDessertFood = baskets.FirstOrDefault(b => b.DessertMenuId == id);
            baskets.Remove(deletedStarterFood);
            baskets.Remove(deletedDessertFood);
            
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }

        public async Task<List<FoodCart>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.FoodCarts.Where(c => c.CartId == cartId).ToListAsync();
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(c => c.foodCarts).FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<CartVM> carts, Starter dbStarter, Dessert dbDessert, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    StarterMenuId = dbStarter.Id,
                    DessertMenuId=dbDessert.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("basket", JsonConvert.SerializeObject(carts));
        }
    }
}
