namespace FinalProject.Models
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int StarterId { get; set; }
        public Starter Starter { get; set; }
        public Dessert Dessert { get; set; }
        public int DessertId { get; set; }
    }
}
