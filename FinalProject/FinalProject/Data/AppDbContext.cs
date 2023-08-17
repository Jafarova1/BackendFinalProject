using FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AppUser> Users { get; set; }
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
        public DbSet<Story> Stories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<FoodComment> FoodComments { get; set; }
        public DbSet<MiniPost> MiniPosts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactBox> ContactBoxes { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<RecentBlog> RecentBlogs { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AboutUs>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Advertisment>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<BlogInfo>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Dessert>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<DessertMenuImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<FoodComment>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Order>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Post>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Recipe>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Starter>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<StarterMenuImage>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Subscriber>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Story>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<MiniPost>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Contact>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<ContactBox>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Social>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<News>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Author>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<RecentBlog>().HasQueryFilter(m => !m.SoftDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDelete);
        }
    }
}
