using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsRememberMe { get; set; }

        public ICollection<FoodComment> ProductComments { get; set; }
        public Basket Basket { get; set; }

    }
}
