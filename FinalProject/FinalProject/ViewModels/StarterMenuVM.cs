using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class StarterMenuVM
    {
        public IEnumerable<StarterMenuImage> Images { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
