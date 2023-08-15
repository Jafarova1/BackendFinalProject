using FinalProject.Helpers;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class StoryVM
    {
        public PaginateData<Story> PaginatedDatas { get; set; }
        public Story Story { get; set; }
        public List<Story> Stories { get; set; }
    }
}
