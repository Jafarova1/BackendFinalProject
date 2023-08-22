using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.DessertMenu
{
    public class DessertMenuCreateVM
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
