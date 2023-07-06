namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using static Common.NotificationMessagesConstants;


    public class LegalAdviseController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ITicketCategoryService ticketCategoryService;
        private readonly ILegalAdviseService legalAdviseService;

        public LegalAdviseController(TicketService _ticketService, ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService)
        {
            this.ticketService = _ticketService;
            this.legalAdvisorService = _legalAdvisorService;
            this.ticketCategoryService = _ticketCategoryService;
            this.legalAdviseService = _legalAdviseService;
        }
        //Add
        [HttpGet]
        public async Task<IActionResult> Add(string ticketId)
        {
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(ticketId);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(ticketId);
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
                TicketForAnswerFormModel model = await this.ticketService.GetTicketForAnswerByIdAsync(ticketId);
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();
                return View(model);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Add
        [HttpPost]
        public async Task<IActionResult> Add(TicketForAnswerFormModel model, string ticketId)
        {
            if (!this.ModelState.IsValid)
            {
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();
                return this.View(model);
            }
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(ticketId);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(ticketId);
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
                await this.legalAdviseService.AddLegalAdviseToTicketByIdAsync(ticketId, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Legal Advise was added successfully!";

            return this.RedirectToAction("Mine", "LegalAdvise");
        }
    }
}
