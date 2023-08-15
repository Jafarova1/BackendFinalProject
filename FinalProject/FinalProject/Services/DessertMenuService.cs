using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class DessertMenuService : IDessertMenuService
    {
        private readonly AppDbContext _context;

        public DessertMenuService(AppDbContext context)
        {
            _context = context;
        }
        //public async Task<IEnumerable<Dessert>> GetAll()
        //{
        //    return await _context.Desserts.Include(m => m.Price).Include(m => m.Description).Include(m => m.Images)
        //        .Include(m => m.Title).ToListAsync();
        //}

        //public async Task<Dessert> GetById(int? id)
        //{
        //    return await _context.Desserts.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        //}

        //public async Task<int> GetCountAsync() => await _context.Starters.CountAsync();


        //public async Task<List<Dessert>> GetFeaturedFoods() => await _context.Desserts.Include(m => m.Images).OrderByDescending(m => m.Price).ToListAsync();


        //public async Task<Dessert> GetFoodByImageId(int? id)
        //{
        //    return await _context.Desserts
        //   .Include(p => p.Images)
        //   .FirstOrDefaultAsync(p => p.Images.Any(p => p.Id == id));
        //}

        //public async Task<DessertMenuImage> GetImageById(int? id)
        //{
        //    return await _context.DessertMenuImages.FindAsync((int)id);
        //}

        //public async Task<List<Dessert>> GetLatestFoods() => await _context.Desserts.Include(m => m.Images).OrderByDescending(m => m.CreatedDate).ToListAsync();

        //public async Task<List<DessertMenuVM>> GetMappedAllFoods()
        //{
        //    List<DessertMenuVM> model = new();
        //    var foods = await _context.Desserts.Include(p => p.Images).ToListAsync();
        //    foreach (var item in foods)
        //    {
        //        model.Add(new DessertMenuVM

        //        {
        //            Id = item.Id,
        //            Title = item.Title,
        //            Description = item.Description,
        //            Images = item.Images,
        //            //Price = item.Price,


        //        });
        //    }
        //    return model;
        //}
  

        public async Task<List<Dessert>> GetAll() => await _context.Desserts.ToListAsync();

        public async Task<List<FoodComment>> GetComments()
        {
            return await _context.FoodComments.Include(p => p.Dessert).ToListAsync();
        }

        public async Task<Dessert> GetDessertMenuById(int? id) => await _context.Desserts.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<DessertMenuImage> GetImageById(int? id)
        {
            return await _context.DessertMenuImages.FindAsync((int)id);
        }
        public async Task<FoodComment> GetCommentById(int? id)
        {
            return await _context.FoodComments.FirstOrDefaultAsync(ac => ac.Id == id);
        }
        public async Task<FoodComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.FoodComments.Include(p => p.Dessert).FirstOrDefaultAsync(ac => ac.Id == id);
        }
    }
}
