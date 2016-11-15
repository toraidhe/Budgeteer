using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budgeteer.Base;
using Microsoft.AspNetCore.Mvc;

namespace Budgeteer.Controllers
{
    [RequireHttps]
    public class HomeController : BudgeteerConroller<HomeController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Help()
        {
            ViewData["Message"] = "Help page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
