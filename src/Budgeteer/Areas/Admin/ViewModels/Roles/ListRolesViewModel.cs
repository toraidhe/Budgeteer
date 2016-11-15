using System.Collections.Generic;
using Budgeteer.Data.Models;

namespace Budgeteer.Areas.Admin.ViewModels.Roles
{
    public class ListRolesViewModel
    {
        public IList<ApplicationRole> ApplicationRoles { get; set; }
    }
}
