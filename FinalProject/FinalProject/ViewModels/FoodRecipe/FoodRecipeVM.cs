using FinalProject.Models;

namespace FinalProject.ViewModels.FoodRecipe
{
    public class FoodRecipeVM
    {
        public FoodComment FoodComment{ get; set; }
        public List<Category> Categories { get; set; }
        public List<RecentBlog> RecentBlogs { get; set; }
        public List<News> Newss { get; set; }
        public Author Author { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public Starter Starter { get; set; }
        public IEnumerable<Starter> Starters { get; set; }
        //public Dessert Starter { get; set; }
        //public IEnumerable<Starter> Starters { get; set; }
        public IEnumerable<FoodComment> FoodComments { get; set; }
    }
}
