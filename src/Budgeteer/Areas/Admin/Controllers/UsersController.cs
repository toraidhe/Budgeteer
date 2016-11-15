using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgeteer.Areas.Admin.ViewModels.Users;
using Budgeteer.Base;
using Budgeteer.Data.Models;
using Budgeteer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Budgeteer.Areas.Admin.Controllers
{
    [Authorize]
    [RequireHttps]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<UsersController>();
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new ListUsersViewModel { ApplicationUsers = _userManager.Users.ToList()};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EditUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = new ApplicationUser() { Id = model.Id, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>  If you do not know your password contact your administrator for assistance.");

                return View();
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpGet]
        public IActionResult Update(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = new ApplicationUser();
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return View("Index");
            }
            AddErrors(result);
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(EditUserViewModel model, string returnUrl = null)
        {
            //TODO:  Check to see if the user is the actively logged in user before deleting the user.l
            var user = new ApplicationUser() { Id = model.Id, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return View("Index");
            }
            AddErrors(result);
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string email, string returnUrl = null)
        {
            //TODO:  Check to see if the user is the actively logged in user before deleting the user.l
            var user = new ApplicationUser() { UserName = email };

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(email, "Reset Password",
               $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return View("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
