using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SubscribeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Subscribe(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ThankYou");
            }

            return View("Index", subscriber);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
