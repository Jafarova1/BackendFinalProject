using FinalProject.Areas.Admin.ViewModels.Order;
using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FinalProject.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IOrderService _orderService;


        public OrderController(AppDbContext context, IWebHostEnvironment env, IOrderService orderService)
        {
            _context = context;
            _env = env;
            _orderService = orderService;



        }
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _orderService.GetAll();
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Order order = await _orderService.GetOrderById(id);
                if (order == null) return NotFound();
                return View(order);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateVM order)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return View(order);
                }

                if (!order.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(order);
                }

                if (!order.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View(order);

                }

                string fileName = Guid.NewGuid().ToString() + " " + order.Photo.FileName;
                string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                await FileHelper.SaveFileAsync(newPath, order.Photo);

                Order newOrder = new()
                {
                    Image = fileName,
                    Title = order.Title,
                    Description = order.Description

                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();
                Order order = await _orderService.GetOrderById(id);
                if (order == null) return NotFound();
                string path = FileHelper.GetFilePath(_env.WebRootPath, "/images", order.Image);
                FileHelper.DeleteFile(path);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {

                if (id == null) return BadRequest();
                Order dbOrder = await _orderService.GetOrderById(id);
                if (dbOrder == null) return NotFound();

                OrderEditVM model = new()
                {
                    Image = dbOrder.Image,
                    Title = dbOrder.Title,
                    Description = dbOrder.Description,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, OrderEditVM order)
        {
            try
            {


                if (id == null) return BadRequest();
                Order dbOrder = await _orderService.GetOrderById(id);
                if (dbOrder == null) return NotFound();

                OrderEditVM model = new()
                {
                    Image = dbOrder.Image,
                    Title = dbOrder.Title,
                    Description = dbOrder.Description,

                };
                if (order.Photo != null)
                {
                    if (!order.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(model);
                    }
                    if (!order.Photo.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 500kb");
                        return View(model);
                    }

                    string deletePath = FileHelper.GetFilePath(_env.WebRootPath, "/images", dbOrder.Image);
                    FileHelper.DeleteFile(deletePath);
                    string fileName = Guid.NewGuid().ToString() + " " + order.Photo.FileName;
                    string newPath = FileHelper.GetFilePath(_env.WebRootPath, "/images", fileName);
                    await FileHelper.SaveFileAsync(newPath, order.Photo);
                    dbOrder.Image = fileName;
                }
                else
                {
                    Order newOrder = new()
                    {
                        Image = dbOrder.Image
                    };
                }

                dbOrder.Title = order.Title;
                dbOrder.Description=order.Description;


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
