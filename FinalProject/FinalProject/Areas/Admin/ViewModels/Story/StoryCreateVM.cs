using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Story
{
    public class StoryCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
     
    
    }

}
