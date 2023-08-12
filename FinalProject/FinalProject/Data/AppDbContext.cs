using FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class AppDbContext:DbContext
    {

            public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
            {

            }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BlogInfo> BlogInfos { get; set; }
        public DbSet<AboutUs> AboutUss { get; set; }
        public DbSet<AboutSlider> AboutSliders { get; set; }
        public DbSet<Starter> Starters { get; set; }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StarterMenuImage> StarterMenuImages { get; set; }
        public DbSet<DessertMenuImage> DessertMenuImages { get; set; }


    }
}
