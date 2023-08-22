using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Author
{
    public class AuthorCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
