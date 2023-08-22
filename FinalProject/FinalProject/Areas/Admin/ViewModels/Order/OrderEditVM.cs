using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Order
{
    public class OrderEditVM
    {
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
