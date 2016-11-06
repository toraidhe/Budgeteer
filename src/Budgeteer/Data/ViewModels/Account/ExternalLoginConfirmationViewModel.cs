using System.ComponentModel.DataAnnotations;

namespace Budgeteer.Data.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
