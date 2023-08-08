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
        public DbSet<Post> Posts { get; set; }
        public DbSet<BlogInfo> BlogInfos { get; set; }

    }
}
