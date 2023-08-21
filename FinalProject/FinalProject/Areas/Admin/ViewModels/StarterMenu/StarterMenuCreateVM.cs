using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.StarterMenu
{
    public class StarterMenuCreateVM
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
