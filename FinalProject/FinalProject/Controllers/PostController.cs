﻿using FinalProject.Data;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class PostController:Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly IMiniPostService _miniPostService;
        public PostController(AppDbContext context,LayoutService layoutService,IMiniPostService miniPostService)
        {
                _context = context;
            _layoutService = layoutService; 
            _miniPostService = miniPostService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            //List<Dessert> desserts = await _context.Desserts.Include(m => m.Images).Take(5).ToListAsync();
            List<MiniPost> posts = await _miniPostService.GetAll();
            List<Advertisment> advertisments = await _context.Advertisments.ToListAsync();
            List<MiniPost> paginatePosts = await _miniPostService.GetPaginatedDatas(page, take);
            int pageCount = await GetPageCountAsync(take);
            PaginateData<MiniPost> paginatedDatas = new(paginatePosts, page, pageCount);
            MiniPostVM model = new()
            {
               MiniPosts=posts,
                PaginatedDatas = paginatedDatas,
                Advertisments = advertisments


            };
            return View(model);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var storyCount = await _miniPostService.GetCountAsync();

            return (int)Math.Ceiling((decimal)storyCount / take);
        }

    }
}
