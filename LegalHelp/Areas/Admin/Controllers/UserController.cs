namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using LegalHelpSystem.Web.ViewModels.User;

    using static LegalHelpSystem.Common.GeneralApplicationConstants;
    using static Common.NotificationMessagesConstants;


    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IUserAdminService userAdminService;
        private readonly IUploaderService uploaderService;
        private readonly ILegalAdvisorService legalAdvisorService;


        public UserController(IUserService _userService, IUserAdminService _userAdminService, IUploaderService _uploaderService, ILegalAdvisorService _legalAdvisorService)
        {
            this.userService = _userService;
            this.userAdminService = _userAdminService;
            this.uploaderService = _uploaderService;
            this.legalAdvisorService = _legalAdvisorService;
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
                await this.userAdminService.CreateUploader(uploaderUserId);
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

        [Route("User/UnmakeUploader")]
        [HttpGet]
        public async Task<IActionResult> UnmakeUploader(string uploaderUserId)
        {
            bool isUploader = await this.uploaderService.UploaderExistsByUserIdAsync(uploaderUserId);
            if (!isUploader)
            {
                this.TempData[ErrorMessage] = "The user is not an uploader!";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            try
            {

                await this.userAdminService.UnmakeUploader(uploaderUserId);
                this.TempData[SuccessMessage] = "You have successfully remove the role of an uploader!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while removing the role of an uploader! Please try again later.";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
        }

        [Route("User/MakeLegalAdvisor")]
        [HttpGet]
        public async Task<IActionResult> MakeLegalAdvisor(string legalAdvisorUserId)
        {
            bool isLegalAdvisor = await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(legalAdvisorUserId);
            if (isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "The user is already a legal advisor!";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            try
            {
                await this.userAdminService.CreateLegalAdvisor(legalAdvisorUserId);
                this.TempData[SuccessMessage] = "You have successfully created a legal advisor!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while creating a legal advisor! Please try again later.";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
        }

        [Route("User/UnmakeLegalAdvisor")]
        [HttpGet]
        public async Task<IActionResult> UnmakeLegalAdvisor(string legalAdvisorUserId)
        {
            bool isLegalAdvisor = await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(legalAdvisorUserId);
            if (!isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "The user is not a legal advisor!";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            try
            {
                await this.userAdminService.UnmakeLegalAdvisor(legalAdvisorUserId);
                this.TempData[SuccessMessage] = "You have successfully remove the role of a legal advisor!";
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while removing the role of a legal advisor! Please try again later.";

                return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
            }
            return this.RedirectToAction("AllUsers", "User", new { Area = AdminAreaName });
        }



    }
}
