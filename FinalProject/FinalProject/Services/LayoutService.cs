﻿using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettingDatas() => _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        public async Task<List<Social>> GetAll() => await _context.Socials.ToListAsync();
    }
}
