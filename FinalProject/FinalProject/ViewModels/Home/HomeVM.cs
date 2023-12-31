﻿using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Comments { get; set; }
        public string Name { get; set; }
        public BlogInfo BlogInfo { get; set; }
        public Dictionary<string, string> SettingDatas { get; set; }
        public AboutUs AboutUs { get; set; }
        public List<AboutSlider> AboutSliders { get; set; }
        public List<Advertisment> Advertisments { get; set; }
 
    }
}
