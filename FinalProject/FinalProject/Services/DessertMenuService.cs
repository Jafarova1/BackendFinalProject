using FinalProject.Areas.Admin.ViewModels.DessertMenu;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class DessertMenuService : IDessertMenuService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DessertMenuService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


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

        public async Task<List<Dessert>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Desserts.Include(m => m.Images)
                                                 .Skip((page - 1) * take)
                                                 .Take(take)
                                                 .ToListAsync();
        }

        public async Task<Dessert> GetByIdWithImagesAsync(int? id) => await _context.Desserts.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);


        public List<Areas.Admin.ViewModels.DessertMenu.DessertMenuVM> GetMappedDatas(List<Dessert> products)
        {
            List<Areas.Admin.ViewModels.DessertMenu.DessertMenuVM> list = new();

            foreach (var product in products)
            {
                list.Add(new Areas.Admin.ViewModels.DessertMenu.DessertMenuVM
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

        public async Task<Dessert> GetWithIncludesAsync(int id)
        {
            return await _context.Desserts.Where(m => m.Id == id)
                               .Include(m => m.Images)
                               .FirstOrDefaultAsync();
        }

        public DessertMenuDetailVM GetMappedData(Dessert product)
        {
            return new DessertMenuDetailVM
            {
                Name = product.Title,
                Description = product.Description,
                Price = product.Price.ToString("0.#####"),
                CreateDate = product.CreatedDate.ToString("dd-MM-yyyy"),
                Images = product.Images.Select(m => m.Image)
            };
        }

        public async Task<int> GetCountAsync() => await _context.Desserts.CountAsync();


        public async Task CreateAsync(DessertMenuCreateVM model)
        {
            List<DessertMenuImage> images = new();

            foreach (var item in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "img");
                images.Add(new DessertMenuImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            decimal decimalPrice = decimal.Parse(model.Price.Replace(".", ","));

            Dessert product = new()
            {
                Title = model.Name,
                Description = model.Description,
                Price = decimalPrice,
                Images = images
            };

            await _context.Desserts.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImageByIdAsync(int id)
        {
            DessertMenuImage image = await _context.DessertMenuImages.FirstOrDefaultAsync(m => m.Id == id);
            _context.DessertMenuImages.Remove(image);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", image.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int productId, DessertMenuEditVM model)
        {
            List<DessertMenuImage> images = new();

            var product = await GetDessertMenuById(productId);

            if (model.NewImages != null)
            {
                foreach (var item in model.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "img");
                    images.Add(new DessertMenuImage { Image = fileName, DessertMenuId = productId });
                }

                await _context.DessertMenuImages.AddRangeAsync(images);
            }


            decimal decimalPrice = decimal.Parse(model.Price.Replace(".", ","));

            product.Title = model.Name;
            product.Description = model.Description;
            product.Price = decimalPrice;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Desserts.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
            _context.Desserts.Remove(product);
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
    }
}
