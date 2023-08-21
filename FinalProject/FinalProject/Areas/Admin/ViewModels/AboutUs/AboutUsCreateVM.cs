using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile SmallPhoto { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile LargePhoto { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string FirstDescription { get; set; }
        public string SecondDescription { get; set; }
    }
}
