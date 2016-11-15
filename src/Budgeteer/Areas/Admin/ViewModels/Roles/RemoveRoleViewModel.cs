using System.ComponentModel.DataAnnotations;

namespace Budgeteer.Areas.Admin.ViewModels.Roles
{
    public class RemoveRoleViewModel
    {
        [Required]
        [Display(Name = "Identity Key")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
