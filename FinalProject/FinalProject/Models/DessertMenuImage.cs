namespace FinalProject.Models
{
    public class DessertMenuImage:BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public int DessertMenuId { get; set; }
        public Dessert Dessert { get; set; }
    }
}
