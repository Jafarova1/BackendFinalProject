namespace FinalProject.Models
{
    public class Starter:BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<StarterMenuImage> Images { get; set; }
    }
}
