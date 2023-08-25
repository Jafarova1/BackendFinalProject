using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        //private readonly AppDbContext _context;
        //private readonly IContactService _contactService;
        //public ContactController(AppDbContext context,IContactService contactService)
        //{
        //        _contactService = contactService;
        //    _context = context;
        //}
        //public async Task<IActionResult> Index()
        //{
        //    var contacts = await _contactService.GetAllAsync();
        //    return View(contacts);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Detail(int? id)
        //{
        //    if (id == null) return BadRequest();
        //    Contact dbcontact = await _contactService.GetByIdAsync((int)id);
        //    if (dbcontact is null) return NotFound();
        //    return View(dbcontact);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    try
        //    {
        //        if (id == null) return BadRequest();
        //        Contact dbcontact = await _contactService.GetByIdAsync((int)id);
        //        if (dbcontact is null) return NotFound();

        //        _context.Contacts.Remove(dbcontact);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.error = ex.Message;
        //        return View();
        //    }
        //}
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Contact> contact = await _context.Contacts.ToListAsync();

            return View(contact);
        }
    }
}
