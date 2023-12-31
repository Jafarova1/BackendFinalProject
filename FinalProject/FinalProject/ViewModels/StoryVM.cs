﻿using FinalProject.Helpers;
using FinalProject.Models;

namespace FinalProject.ViewModels
{
    public class StoryVM
    {
        public PaginateData<Story> PaginatedDatas { get; set; }
        public List<Advertisment> Advertisments { get; set; }
        public List<Story> Stories { get; set; }
    }
}
