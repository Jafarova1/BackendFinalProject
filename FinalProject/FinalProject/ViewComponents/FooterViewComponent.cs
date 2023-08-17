﻿using FinalProject.Services.Interfaces;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly ISocialService _socialService;

        public FooterViewComponent(ILayoutService layoutService, ISocialService socialService)
        {
            _layoutService = layoutService;
            _socialService = socialService; 
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = new FooterVM()
            {
                SettingDatas = _layoutService.GetSettingDatas(),
                Socials = await _socialService.GetAll()
            };

            return await Task.FromResult(View(model));
        }
    }
}
