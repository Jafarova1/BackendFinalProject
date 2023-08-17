namespace FinalProject.Models
{
    public class News :BaseEntity
    {
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public string FirstDescription { get; set; }
        public string SecondDescription { get; set; }
        public string Image { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<FoodComment> FoodComments { get; set; }

    }
}
