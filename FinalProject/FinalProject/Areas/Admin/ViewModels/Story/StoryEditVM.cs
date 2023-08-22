using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Story
{
    public class StoryEditVM
    {
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Image { get; set; }
  
    }
}
