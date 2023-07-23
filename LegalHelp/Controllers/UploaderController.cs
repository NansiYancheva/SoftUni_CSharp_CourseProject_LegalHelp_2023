namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Uploader;

    using static Common.NotificationMessagesConstants;


    public class UploaderController : BaseController
    {
        private readonly IUploaderService uploaderService;

        public UploaderController (IUploaderService _uploaderService)
        {
            this.uploaderService = _uploaderService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isUploader = await this.uploaderService.UploaderExistsByUserIdAsync(userId);
            if (isUploader)
            {
                this.TempData[ErrorMessage] = "You are already an uploader!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeUploaderFormModel model)
        {
            string? userId = this.User.GetId();
            bool isUploader = await this.uploaderService.UploaderExistsByUserIdAsync(userId);
            if (isUploader)
            {
                this.TempData[ErrorMessage] = "You are already an uploader!";

                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.uploaderService.Create(userId, model);
                this.TempData[SuccessMessage] = "You have successfully become an uploader!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as an uploader! Please try again later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            //to be changed - "All" Documents
            return this.RedirectToAction("Index", "Home");
        }
    }
}
