using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
   
        public ContactController(AppDbContext context )
        {
            _context = context;
       
        }
        public async Task<IActionResult> Index()
        {
    
        
            Dictionary<string, string> settings = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            List<ContactBox> contactBoxes = await _context.ContactBoxes.ToListAsync();

            ContactVM model = new()
            {
             
                Settings = settings,
                ContactBoxes = contactBoxes
               
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
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
            return RedirectToAction("Index");
        }
    }

}

