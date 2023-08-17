﻿using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels.FoodRecipe
{
    public class FoodCommentVM
    {
        [Required(ErrorMessage = "Don`t be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        public string Message { get; set; }
        [Required(ErrorMessage = "Don`t be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
