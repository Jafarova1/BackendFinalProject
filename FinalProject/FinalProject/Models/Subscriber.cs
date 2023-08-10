using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Subscriber:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
