using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Budgeteer.Data.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
