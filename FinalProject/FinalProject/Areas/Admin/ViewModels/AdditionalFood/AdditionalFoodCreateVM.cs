﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModels.AdditionalFood
{
    public class AdditionalFoodCreateVM
    {
        [Required(ErrorMessage = "Don't be empty")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Don't be empty")]
        public string Description { get; set; }
    }
}
