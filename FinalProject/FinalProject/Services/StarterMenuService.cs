using FinalProject.Areas.Admin.ViewModels.StarterMenu;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class StarterMenuService : IStarterMenuService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public StarterMenuService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(StarterMenuCreateVM model)
        {
            List<StarterMenuImage> images = new();

            foreach (var item in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "img");
                images.Add(new StarterMenuImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            decimal decimalPrice = decimal.Parse(model.Price.Replace(".", ","));

            Starter product = new()
            {
                Title = model.Name,
                Description = model.Description,
                Price = decimalPrice,
                Images = images
            };

            await _context.Starters.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Starters.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
            _context.Starters.Remove(product);
            await _context.SaveChangesAsync();

            foreach (var item in product.Images)
            {
                string path = Path.Combine(_env.WebRootPath, "img", item.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        
    }

        public async Task DeleteImageByIdAsync(int id)
        {
            StarterMenuImage image = await _context.StarterMenuImages.FirstOrDefaultAsync(m => m.Id == id);
            _context.StarterMenuImages.Remove(image);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", image.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int productId, StarterMenuEditVM model)
        {
            List<StarterMenuImage> images = new();

            var product = await GetStarterMenuById(productId);

            if (model.NewImages != null)
            {
                foreach (var item in model.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "img");
                    images.Add(new StarterMenuImage { Image = fileName, StarterMenuId = productId });
                }

                await _context.StarterMenuImages.AddRangeAsync(images);
            }


            decimal decimalPrice = decimal.Parse(model.Price.Replace(".", ","));

            product.Title = model.Name;
            product.Description = model.Description;
            product.Price = decimalPrice;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Starter>> GetAll() => await _context.Starters.ToListAsync();

        public async Task<Starter> GetByIdWithImagesAsync(int? id) => await _context.Starters.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<FoodComment> GetCommentById(int? id)
        {
            return await _context.FoodComments.FirstOrDefaultAsync(ac => ac.Id == id);
        }

        public async Task<FoodComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.FoodComments.Include(p => p.Starter).FirstOrDefaultAsync(ac => ac.Id == id);
        }

        public async Task<List<FoodComment>> GetComments()
        {
            return await _context.FoodComments.Include(p => p.Starter).ToListAsync();
        }

        public async Task<int> GetCountAsync() => await _context.Starters.CountAsync();


        public async Task<StarterMenuImage> GetImageById(int? id)
        {
            return await _context.StarterMenuImages.FindAsync((int)id);
        }

        public StarterMenuDetailVM GetMappedData(Starter product)
        {
            return new StarterMenuDetailVM
            {
                Name = product.Title,
                Description = product.Description,
                Price = product.Price.ToString("0.#####"),
                CreateDate = product.CreatedDate.ToString("dd-MM-yyyy"),
                Images = product.Images.Select(m => m.Image)
            };
        }

        public List<Areas.Admin.ViewModels.StarterMenu.StarterMenuVM> GetMappedDatas(List<Starter> products)
        {
            List<Areas.Admin.ViewModels.StarterMenu.StarterMenuVM> list = new();

            foreach (var product in products)
            {
                list.Add(new Areas.Admin.ViewModels.StarterMenu.StarterMenuVM
                {
                    Id = product.Id,
                    Name = product.Title,
                    Description = product.Description,
                    Price = product.Price.ToString("0.#####") + " ₼",
                    Image = product.Images.Where(m => m.IsMain).FirstOrDefault().Image
                });
            }

            return list;
        }

        public async Task<List<Starter>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Starters.Include(m => m.Images)
                                               .Skip((page - 1) * take)
                                               .Take(take)
                                               .ToListAsync();
        }

        public async Task<Starter> GetStarterMenuById(int? id) => await _context.Starters.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Starter> GetWithIncludesAsync(int id)
        {
            return await _context.Starters.Where(m => m.Id == id)
                                         .Include(m => m.Images)
                                         .FirstOrDefaultAsync();
        }
    }
}
