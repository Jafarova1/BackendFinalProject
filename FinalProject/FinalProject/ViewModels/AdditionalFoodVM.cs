using FinalProject.Helpers;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class AdditionalFoodVM
    {
        public PaginateData<AdditionalFood> AddFoods { get; set; }
        public List<Advertisment> Advertisments { get; set; }
        public List<AdditionalFood> AdditionalFoods { get; set; }
    }
}
