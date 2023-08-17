using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class FoodRecipeService : IFoodRecipeService
    {
        private readonly AppDbContext _context;

        public FoodRecipeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<News>> GetAll() => await _context.Newss.Include(m => m.Author)
                                                                         .ToListAsync();


        public async Task<News> GetById(int? id) => await _context.Newss.Include(m => m.Author)
                                                                         .Include(m => m.FoodComments)
                                                                         .FirstOrDefaultAsync(m => m.Id == id);


        public async Task<FoodComment> GetCommentById(int? id)
        {
            return await _context.FoodComments.FindAsync(id);
        }

        public async Task<FoodComment> GetCommentByIdWithNews(int? id)
        {
            return await _context.FoodComments.Include(b => b.Starter).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<FoodComment>> GetComments()
        {
            return await _context.FoodComments.Include(b => b.Starter).ToListAsync();
        }
    }
}
