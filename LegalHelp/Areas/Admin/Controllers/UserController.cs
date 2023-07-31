namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.User;

    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            this.userService = _userService;
        }
        [Route("User/AllUsers")]
        public async Task <IActionResult> AllUsers()
        {
            IEnumerable<AllTeamMembersViewModel> viewModel =
                await this.userService.GetAllUsers();

            return View(viewModel);

        }
    }
}
