using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class FoodVM
    {
   
        //public List<Starter> Starters { get; set; }
        //public List<Dessert> Desserts { get; set; }
        //public decimal Price { get; set; }
        //public int Id { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }

        public List<Starter> Starter { get; set; }
        public List<Dessert> Dessert { get; set; }
        public Order Order { get; set; }
        public ICollection<StarterMenuImage> StarterMenuImages { get; set; }
        public ICollection<DessertMenuImage> DessertMenuImages { get; set; }
        public ICollection<FoodComment> FoodComments { get; set; }

    }
}
