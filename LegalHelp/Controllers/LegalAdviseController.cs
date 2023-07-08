namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

   // using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;

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
                LegalAdviseFormModel legalAdviseFormModel = new LegalAdviseFormModel();
                legalAdviseFormModel.TicketSubject = await this.ticketService.GetTicketSubjectAsync(id);
                legalAdviseFormModel.TicketDescription = await this.ticketService.GetTicketDescription(id);
                legalAdviseFormModel.TicketId = id;

                return View(legalAdviseFormModel);
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
        public async Task<IActionResult> Add (LegalAdviseFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
               
                return this.View(model);
            }
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(model.TicketId);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(model.TicketId);
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
                string? legalAdvisorId = await this.legalAdvisorService.GetLegalAdvisorIdByUserIdAsync(this.User.GetId()!);

                string legalAdviseId = await legalAdviseService.AddLegalAdviseAsync(model, legalAdvisorId!);
                //тук дали да не е без асинк, за да може да изчака да се запази в базата, защото после ще трябва да се търси legal advise по Id
                await this.ticketService.AddLegalAdviseToTicketByIdAsync(model.TicketId, legalAdviseId);

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Legal Advise was added successfully!";

            //return this.RedirectToAction("Mine", "LegalAdvise");
            return this.RedirectToAction("All", "Ticket");
        }
    }
}
