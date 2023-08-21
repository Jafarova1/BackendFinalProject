using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.About
{
    public class AboutSliderUpdateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string ShortDesc { get; set; }
        public string Image { get; set; }
    }
}
