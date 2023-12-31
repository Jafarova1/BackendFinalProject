﻿using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll() => await _context.Categories.ToListAsync();

        public async Task<Category> GetCategoryById(int? id) => await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
    }
}
