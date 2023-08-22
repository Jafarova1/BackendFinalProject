namespace FinalProject.Models
{
    public class Dessert:BaseEntity
    {

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IEnumerable<DessertMenuImage> Images { get; set; }
    }
}
