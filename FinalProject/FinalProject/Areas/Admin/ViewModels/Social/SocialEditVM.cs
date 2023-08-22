using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Social
{
    public class SocialEditVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public string Icon { get; set; }

        [Required(ErrorMessage = "Don't be empty")]
        public string Link { get; set; }
    }
}
