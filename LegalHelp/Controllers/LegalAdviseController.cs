namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using static Common.NotificationMessagesConstants;


    public class LegalAdviseController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ITicketCategoryService ticketCategoryService;
        private readonly ILegalAdviseService legalAdviseService;

        public LegalAdviseController(ITicketService _ticketService, ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService)
        {
            this.ticketService = _ticketService;
            this.legalAdvisorService = _legalAdvisorService;
            this.ticketCategoryService = _ticketCategoryService;
            this.legalAdviseService = _legalAdviseService;
        }
        //Add
        [HttpGet]
        public async Task<IActionResult> Add(string id)
        {

            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(id);
            if (resolvedStatus == true)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id was already resolved!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool isLegalAdvisor =
                await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "You must become a legal advisor in order to answer legal question!";

                return this.RedirectToAction("Become", "LegalAdvisor");
            }

            try
            {
                TicketForAnswerFormModel model = await this.ticketService.GetTicketForAnswerByIdAsync(id);
               
                return View(model);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Add
        //до тук добре, обаче тук не ми помни модела
        //в legal advise add.cshtml - пак като при адд гет - да се взима, но не само ид, а целия модел - как?
        //does the URL changes with some info for the model or only with the ID
        [HttpPost]
        public async Task<IActionResult> Add(TicketForAnswerFormModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
               
                return this.View(model);
            }
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(id);
            if (resolvedStatus == true)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id was already resolved!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool isLegalAdvisor =
                await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "You must become a legal advisor in order to answer legal question!";

                return this.RedirectToAction("Become", "LegalAdvisor");
            }
            try
            {
                await this.legalAdviseService.AddLegalAdviseToTicketByIdAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Legal Advise was added successfully!";

            return this.RedirectToAction("Mine", "LegalAdvise");
        }
    }
}
