using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsUpdateVM
    {
        public IFormFile SmallPhoto { get; set; }

        public IFormFile LargePhoto { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string FirstDescription { get; set; }
        public string SecondDescription { get; set; }
        public string FirstImage { get; set; }
        public string SecondImage { get; set; }
    }
}
