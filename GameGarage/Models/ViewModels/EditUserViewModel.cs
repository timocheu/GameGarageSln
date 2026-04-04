using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameGarage.Models.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
            AllRoles = new List<string>();
        }

        public string? Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public IList<string> Roles { get; set; }

        public IList<string> AllRoles { get; set; }
    }
}
