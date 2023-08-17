using FinalProject.Models;

namespace FinalProject.ViewModels.FoodRecipe
{
    public class FoodRecipeVM
    {
        public FoodCommentVM FoodCommentVM { get; set; }
        public List<Category> Categories { get; set; }
        public List<RecentBlog> RecentBlogs { get; set; }
        public List<News> Newss { get; set; }
        public Author Author { get; set; }
    }
}
