using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.RecentBlog
{
    public class RecentBlogEditVM
    {
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Image { get; set; }
    }
}
