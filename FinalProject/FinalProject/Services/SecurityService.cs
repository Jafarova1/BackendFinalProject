//using FinalProject.Models;

//namespace FinalProject.Services
//{
//    public class SecurityService
//    {
//        List<AppUser> knownUsers=new List<AppUser>();
//        public SecurityService()
//        {
//                knownUsers.Add(new AppUser { Id=0,UserName="Gultaj",Password="gultaj123"});
//            knownUsers.Add(new AppUser { Id = 1, UserName = "Aytaj", Password = "aytaj123" });
//            knownUsers.Add(new AppUser { Id = 2, UserName = "Nur", Password = "nur123" });
//        }
//        public bool IsValid(AppUser user)
//        {
//            return knownUsers.Any(x=>x.UserName
//            == user.UserName && x.Password==user.Password);
//        }
//    }
//}
