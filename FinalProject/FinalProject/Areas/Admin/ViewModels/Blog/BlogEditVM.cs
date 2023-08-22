﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.Blog
{
    public class BlogEditVM
    {
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
    }
}
