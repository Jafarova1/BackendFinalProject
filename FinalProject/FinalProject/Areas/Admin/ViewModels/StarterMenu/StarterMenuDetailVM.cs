namespace FinalProject.Areas.Admin.ViewModels.StarterMenu
{
    public class StarterMenuDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string Price { get; set; }
        public IEnumerable<string> Images { get; set; }
   
    }
}
