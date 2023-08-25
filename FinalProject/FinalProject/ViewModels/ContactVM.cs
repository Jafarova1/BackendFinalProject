using FinalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class ContactVM
    {
        public Dictionary<string, string> Settings { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Message { get; set; }
        public List<ContactBox> ContactBoxes { get; set; }
        public Contact Contact { get; set; }
    }
}
