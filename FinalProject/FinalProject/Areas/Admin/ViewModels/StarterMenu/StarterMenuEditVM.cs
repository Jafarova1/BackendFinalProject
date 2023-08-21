using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.StarterMenu
{
    public class StarterMenuEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }

        public List<IFormFile> NewImages { get; set; }
        public List<StarterMenuImage> Images { get; set; }
    }
}
