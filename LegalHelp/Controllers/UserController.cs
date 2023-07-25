namespace LegalHelpSystem.Web.Controllers
{
   // using Griesoft.AspNetCore.ReCaptcha;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using ViewModels.User;
    using LegalHelpSystem.Services.Data.Interfaces;

    using static Common.NotificationMessagesConstants;


    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public UserController(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              IUserService userService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]

        [HttpPost]
        //[ValidateRecaptcha(Action = nameof(Register),
        //    ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                TempData[ErrorMessage] =
                    "There was an error while logging you in! Please try again later or contact an administrator.";

                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }

        //AllTeamMembers
        public async Task<IActionResult> AllTeamMembers()
        {
            IEnumerable<AllTeamMembersViewModel> model = await userService.GetAllTeamMembers();
            return View(model);
        }
    }
}

