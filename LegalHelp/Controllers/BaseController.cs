namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class BaseController : Controller
    {
        //Common
        public IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
