using System.ComponentModel.DataAnnotations;

namespace Budgeteer.Areas.Admin.ViewModels.Roles
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
