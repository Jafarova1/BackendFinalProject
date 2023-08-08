using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public BlogInfo BlogInfo { get; set; }
        public int Id { get; internal set; }
    }
}
