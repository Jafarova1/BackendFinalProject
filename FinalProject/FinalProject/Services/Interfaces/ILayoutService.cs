﻿using FinalProject.Models;
using FinalProject.ViewModels;

namespace FinalProject.Services.Interfaces
{
    public interface ILayoutService
    {
        Dictionary<string, string> GetSettingDatas();
        Task<List<Social>> GetAll();
    }
}
