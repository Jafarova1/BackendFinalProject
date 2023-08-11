﻿using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAboutSliderService _aboutSliderService;
 
        public HomeController(AppDbContext context,IBlogService blogService,IAboutSliderService aboutSliderService)
        {
            _context = context;
            _aboutSliderService = aboutSliderService;
       
            
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDelete).ToListAsync();
            IEnumerable<Post> posts = await _context.Posts.Where(m => !m.SoftDelete).ToListAsync();
            IEnumerable<Recipe> recipes = await _context.Recipes.Where(m => !m.SoftDelete).ToListAsync();
            List<AboutSlider> sliders = await _aboutSliderService.GetAll();
            AboutUs aboutUs = await _context.AboutUss.FirstOrDefaultAsync();
            HomeVM model = new()
            {
                Blogs = blogs,
                Posts = posts,
                Recipes = recipes,
                AboutSliders = sliders,
                AboutUs=aboutUs
                

                

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(SubscribeVM model)
        {
            try
            {
                if (!ModelState.IsValid) return RedirectToAction("Index", model);
                var existSubscribe = await _context.Subscribers.FirstOrDefaultAsync(m => m.Email == model.Email);
                if (existSubscribe != null)
                {
                    ModelState.AddModelError("Email", "Email already exist");
                    return RedirectToAction("Index");
                }
                Subscriber subscribe = new()
                {
                    Email = model.Email,
                };
                await _context.Subscribers.AddAsync(subscribe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }



    }
}