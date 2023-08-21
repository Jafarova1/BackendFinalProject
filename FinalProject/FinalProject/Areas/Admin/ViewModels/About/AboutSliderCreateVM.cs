using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.AboutSlider
{
    public class AboutSliderCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string ShortDesc { get; set; }


    }
}
