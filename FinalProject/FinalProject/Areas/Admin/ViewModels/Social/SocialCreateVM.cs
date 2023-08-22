using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Social
{
    public class SocialCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Icon { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Link { get; set; }
    }
}
