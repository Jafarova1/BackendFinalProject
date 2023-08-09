using FinalProject.Models;

namespace FinalProject.Services
{
    public class SecurityService
    {
        List<User> knownUsers=new List<User>();
        public SecurityService()
        {
                knownUsers.Add(new User { Id=0,UserName="Gultaj",Password="gultaj123"});
            knownUsers.Add(new User { Id = 1, UserName = "Aytaj", Password = "aytaj123" });
            knownUsers.Add(new User { Id = 2, UserName = "Nur", Password = "nur123" });
        }
        public bool IsValid(User user)
        {
            return knownUsers.Any(x=>x.UserName
            == user.UserName && x.Password==user.Password);
        }
    }
}
