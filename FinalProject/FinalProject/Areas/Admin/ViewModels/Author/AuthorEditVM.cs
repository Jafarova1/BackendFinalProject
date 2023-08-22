using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Author
{
    public class AuthorEditVM
    {
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
