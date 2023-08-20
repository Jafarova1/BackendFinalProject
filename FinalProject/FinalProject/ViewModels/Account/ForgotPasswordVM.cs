using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
