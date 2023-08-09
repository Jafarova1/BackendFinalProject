﻿using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
        public BlogInfo BlogInfo { get; set; }
        public Dictionary<string, string> SettingDatas { get; set; }
    }
}
