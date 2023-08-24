namespace FinalProject.Models
{
    public class StarterMenuImage:BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public int StarterMenuId { get; set; }
        public Starter Starter { get; set; }


     
    }
}
