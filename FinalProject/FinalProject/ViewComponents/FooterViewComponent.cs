using FinalProject.Services;
using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly LayoutService _layoutService;
        private readonly ISocialService _socialService;

        public FooterViewComponent(LayoutService layoutService, ISocialService socialService)
        {
            _layoutService = layoutService;
            _socialService = socialService; 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
                Dictionary<string, string> settings = await _layoutService.GetSettingDatas();
            FooterVM model = new FooterVM()
            {
                SettingDatas = settings,
                Socials = await _socialService.GetAll()
            };

            return await Task.FromResult(View(model));
        }
    }
}
