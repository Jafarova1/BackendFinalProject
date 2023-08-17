using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class ContactBoxService : IContactBoxService
    {
        private readonly AppDbContext _context;
        public ContactBoxService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ContactBox> GetAboutBoxById(int? id) => await _context.ContactBoxes.FirstOrDefaultAsync(m => m.Id == id);


        public async Task<List<ContactBox>> GetAll() => await _context.ContactBoxes.ToListAsync();

    }
}
