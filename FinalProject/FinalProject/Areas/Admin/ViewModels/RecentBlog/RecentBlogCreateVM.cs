using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.RecentBlog
{
    public class RecentBlogCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
