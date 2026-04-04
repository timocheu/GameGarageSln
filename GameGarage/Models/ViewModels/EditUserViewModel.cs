using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameGarage.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
