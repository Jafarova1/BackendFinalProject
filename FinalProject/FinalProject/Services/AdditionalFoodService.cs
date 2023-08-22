using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AdditionalFoodService:IAdditionalFoodService
    {
        private readonly AppDbContext _context;

        public AdditionalFoodService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AdditionalFood>> GetAll() => await _context.AdditionalFoods.ToListAsync();

        public async Task<AdditionalFood> GetById(int? id) => await _context.AdditionalFoods.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<int> GetCountAsync() => await _context.AdditionalFoods.CountAsync();

        public async Task<List<AdditionalFood>> GetPaginatedDatas(int page, int take)
        {
            return await _context.AdditionalFoods.Skip((page * take) - take).Take(take).ToListAsync();
        }

     
    }
}
