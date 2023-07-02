namespace LegalHelpSystem.Web.Controllers
{
    //using Microsoft.AspNetCore.Mvc;

    //using static Common.NotificationMessagesConstants;


    public class LegalAdvisorController : BaseController
    {
        //private readonly ILegalAdvisorService legalAdvisorService;

        public LegalAdvisorController ()//(ILegalAdvisorService _legalAdvisorService)
        {
            //this.legalAdvisorService = _legalAdvisorService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Become()
        //{
        //    string? userId = this.User.GetId();
        //    bool isLegalAdvisor = await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(userId);
        //    if (isLegalAdvisor)
        //    {
        //        this.TempData[ErrorMessage] = "You are already a legal advisor!";

        //        return this.RedirectToAction("Index", "Home");
        //    }

        //    return this.View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Become(BecomeLegalAdvisorFormModel model)
        //{
        //    string? userId = this.User.GetId();
        //    bool isLegalAdvisor = await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(userId);
        //    if (isLegalAdvisor)
        //    {
        //        this.TempData[ErrorMessage] = "You are already a legal advisor!";

        //        return this.RedirectToAction("Index", "Home");
        //    }

        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    try
        //    {
        //        await this.legalAdvisorService.Create(userId, model);
        //    }
        //    catch (Exception)
        //    {
        //        this.TempData[ErrorMessage] =
        //            "Unexpected error occurred while registering you as a legal advisor! Please try again later or contact administrator.";

        //        return this.RedirectToAction("Index", "Home");
        //    }

        //    //to be changed - "All" LegalAdvises
        //    return this.RedirectToAction("Index", "Home");
        //}
        }
    }

