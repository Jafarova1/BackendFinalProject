using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]

        public string Title { get; set; }
    }
}
