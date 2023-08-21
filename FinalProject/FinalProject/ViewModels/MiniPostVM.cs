using FinalProject.Helpers;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class MiniPostVM
    {
        public PaginateData<MiniPost> PaginatedDatas { get; set; }
        public List<Advertisment> Advertisments { get; set; }
        public List<MiniPost> MiniPosts { get; set; }
        public string Email { get; set; }
    }
}
