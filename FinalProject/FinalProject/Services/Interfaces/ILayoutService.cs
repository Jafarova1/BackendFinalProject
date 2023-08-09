using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutVM> GetAllDatas();
    }
}
