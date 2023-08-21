using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.StarterMenu
{
    public class StarterMenuVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
