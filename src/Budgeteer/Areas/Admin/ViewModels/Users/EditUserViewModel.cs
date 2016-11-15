using System.ComponentModel.DataAnnotations;

namespace Budgeteer.Areas.Admin.ViewModels.Users
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Identity Key")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
