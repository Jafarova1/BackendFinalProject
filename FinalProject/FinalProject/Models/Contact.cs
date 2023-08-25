using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Contact:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
