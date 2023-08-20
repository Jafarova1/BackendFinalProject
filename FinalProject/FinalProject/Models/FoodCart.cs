namespace FinalProject.Models
{
    public class FoodCart:BaseEntity
    {
        public int Count { get; set; }
        public int StarterMenuId { get; set; }
        public int DessertMenuId { get; set; }
        public int CartId { get; set; }
        public Starter Starter { get; set; }
        public Dessert Dessert { get; set; }

    }
}
