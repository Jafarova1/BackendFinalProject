using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class StarterMenuService : IStarterMenuService
    {
        private readonly AppDbContext _context;

        public StarterMenuService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Starter>> GetAll()
        {
           return await _context.Starters.Include(m => m.Price).Include(m => m.Description).Include(m => m.Images)
                .Include(m => m.Title).ToListAsync();
        }

        public async Task<Starter> GetById(int? id)
        {
            return await _context.Starters.Include(p=>p.Images).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> GetCountAsync() => await _context.Starters.CountAsync();


        public async Task<List<Starter>> GetFeaturedFoods()=>await _context.Starters.Include(m => m.Images).OrderByDescending(m => m.Price).ToListAsync();


        public async Task<StarterMenuImage> GetImageById(int? id)
        {
            return await _context.StarterMenuImages.FindAsync((int)id);
        }

        public async Task<List<Starter>> GetLatestFoods() => await _context.Starters.Include(m => m.Images).OrderByDescending(m => m.CreatedDate).ToListAsync();


        public async Task<List<StarterMenuVM>> GetMappedAllFoods()
        {
            List<StarterMenuVM> model = new();
            var foods = await _context.Starters.Include(p => p.Images).ToListAsync();
            foreach (var item in foods)
            {
                model.Add(new StarterMenuVM

                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Images = item.Images,
                    Price = item.Price,


                });
            }
            return model;
        }

        public async Task<Starter> GetFoodByImageId(int? id)
        {
            return await _context.Starters
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Images.Any(p => p.Id == id));
        }
    }
}
