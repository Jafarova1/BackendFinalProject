using FinalProject.Models;
using FinalProject.ViewModels.Cart;

namespace FinalProject.Services.Interfaces
{
    public interface ICartService
    {
        List<CartVM> GetDatasFromCookie();
        void SetDatasToCookie(List<CartVM> carts, Starter dbStarter,Dessert dbDessert, CartVM existProduct);
        void DeleteData(int? id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<FoodCart>> GetAllByCartIdAsync(int? cartId);
    }
}
