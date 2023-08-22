using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public int Number { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Name { get; set; }
    }
}
