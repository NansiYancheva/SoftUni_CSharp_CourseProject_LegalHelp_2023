namespace LegalHelp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    using LegalHelpSystem.Web.Controllers;
    using LegalHelpSystem.Web.ViewModels.Home;

    using static LegalHelpSystem.Common.GeneralApplicationConstants;

    public class HomeController : BaseController
    {
        public HomeController()
        {

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if(this.User.IsInRole(AdminRoleName))
            {
               return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName});
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}