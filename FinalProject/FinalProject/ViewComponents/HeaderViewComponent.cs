using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
                _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                SettingDatas = _layoutService.GetSettingDatas(),
            };
            return await Task.FromResult(View(model));
        }
    }
}
