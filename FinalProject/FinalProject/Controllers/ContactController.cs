using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;
        private readonly IContactService _contactService;
        private readonly IContactBoxService _contactBoxService;
        public ContactController(AppDbContext context,ILayoutService layoutService,IContactService contactService,IContactBoxService contactBoxService)
        {
            _context = context;
            _layoutService = layoutService;
            _contactService = contactService;
            _contactBoxService = contactBoxService;
                
        }

        public async Task<IActionResult> Index()
        {
            List<ContactBox> contactBoxes = await _contactBoxService.GetAll();
            ContactVM model = new()
            {
                ContactBoxes = contactBoxes,
                Settings = _layoutService.GetSettingDatas(),


            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PostComment(ContactVM model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", model);
            Contact contact = new()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
