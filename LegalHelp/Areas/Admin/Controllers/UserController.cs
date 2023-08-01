namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.User;

    using static LegalHelpSystem.Common.GeneralApplicationConstants;
    using static Common.NotificationMessagesConstants;


    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IUploaderService uploaderService;

        public UserController(IUserService _userService, IUploaderService _uploaderService)
        {
            this.userService = _userService;
            this.uploaderService = _uploaderService;
        }
        [Route("User/AllUsers")]
        public async Task <IActionResult> AllUsers()
        {
            IEnumerable<AllTeamMembersViewModel> viewModel =
                await this.userService.GetAllUsers();

            return View(viewModel);
        }

        [Route("User/MakeUploader")]
        [HttpGet]
        public async Task<IActionResult> MakeUploader(string uploaderUserId)
        {
            bool isUploader = await this.uploaderService.UploaderExistsByUserIdAsync(uploaderUserId);
            if (isUploader)
            {
                this.TempData[ErrorMessage] = "The user is already an uploader!";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            try
            {
                await this.uploaderService.Create(uploaderUserId);
                this.TempData[SuccessMessage] = "You have successfully created an uploader!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while creating an uploader! Please try again later.";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
        }




    }
}
