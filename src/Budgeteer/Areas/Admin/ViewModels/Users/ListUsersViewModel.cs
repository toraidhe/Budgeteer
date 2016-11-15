using System.Collections.Generic;
using Budgeteer.Data.Models;

namespace Budgeteer.Areas.Admin.ViewModels.Users
{
    public class ListUsersViewModel
    {
        public IList<ApplicationUser> ApplicationUsers { get; set; }

        //public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}
