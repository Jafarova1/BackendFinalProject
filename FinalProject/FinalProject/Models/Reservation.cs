namespace FinalProject.Models
{
    public class Reservation:BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comments { get; set; }
    }
}
