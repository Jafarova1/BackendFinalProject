namespace FinalProject.Models
{
    public class FoodComment:BaseEntity
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public int StarterMenuId { get; set; }
        public int DessertMenuId { get; set; }
        public Starter Starter { get; set; }
        public Dessert Dessert { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
