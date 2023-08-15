using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAll() => await _context.Orders.ToListAsync();


        public async Task<Order> GetOrderById(int? id) => await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

    }
}
