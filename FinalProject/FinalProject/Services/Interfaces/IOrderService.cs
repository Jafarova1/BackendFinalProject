using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<Order> GetOrderById(int? id);
    }
}
