namespace FinalProject.Models
{
    public class Author :BaseEntity
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<News> Newss { get; set; }

    }
}
