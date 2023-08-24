namespace FinalProject.ViewModels.Basket
{
    public class BasketProductVM
    {

        public int Id { get; set; }
        public int StarterMenuId { get; set; }
        public int DessertMenuId { get; set; }
        public string StarterImage { get; set; }
        public string DessertImage { get; set; }
        public int Quantity { get; set; }
        public decimal StarterMenuPrice { get; set; }
        public decimal DessertMenuPrice { get; set; }
        public decimal StarterTotal { get; set; }
        public decimal DessertTotal { get; set; }
    }
}
