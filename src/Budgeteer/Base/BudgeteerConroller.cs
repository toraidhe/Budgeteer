#region License
// Project:  Budgeteer
// Written By:  Tory Bohn
// Written On:  11 12 2016
#endregion

using System.Threading.Tasks;
using Budgeteer.Areas.Admin.Controllers;
using Budgeteer.Controllers;
using Budgeteer.Data.Models;
using Budgeteer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Budgeteer.Base
{
    public abstract class BudgeteerConroller<T> : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IEmailSender _emailSender;
        protected readonly ISmsSender _smsSender;
        protected readonly ILogger _logger;

        protected BudgeteerConroller(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<T>();
        }

        protected BudgeteerConroller()
        {
            _userManager = null;
            _emailSender = null;
            _smsSender = null;
            _logger = null;
        }
        #region Helpers

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        protected Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(SecurityController.Index), nameof(SecurityController).ToString());
        //    }
        //}

        #endregion
    }
}