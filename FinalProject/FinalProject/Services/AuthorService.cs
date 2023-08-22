using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetAll() => await _context.Authors.ToListAsync();
        public async Task<Author> GetById(int? id) => await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);
    }
}
