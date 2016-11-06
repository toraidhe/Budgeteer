using System.ComponentModel.DataAnnotations;

namespace Budgeteer.Data.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
