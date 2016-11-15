using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgeteer.Areas.Admin.ViewModels.Roles;
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
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public RolesController(
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<RolesController>();
        }

        //
        //GET:  /Admin/Role/
        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new ListRolesViewModel {ApplicationRoles = _roleManager.Roles.ToList()};
            
            return View(model);
        }

        //
        //GET:  /Admin/Role/Create
        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        //POST:  /Admin/Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var role = new ApplicationRole() { Name = model.Name, Description = model.Description };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return View();
            }
            AddErrors(result);
            return View();
        }

        //
        //GET:  /Admin/Role/Update
        [HttpGet]
        public IActionResult Update(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }

        //
        //POST:  /Admin/Role/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var role = new ApplicationRole() { Name = model.Name, Description = model.Description };
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return View("Index");
            }
            AddErrors(result);
            return View("Index");
        }

        //
        //POST:  /Admin/Role/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string returnUrl = null)
        {
            var model = new RemoveRoleViewModel();
            var role = new ApplicationRole() { Name = model.Name, Description = model.Description, Id = model.Id };
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return View("Index");
            }
            AddErrors(result);
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
