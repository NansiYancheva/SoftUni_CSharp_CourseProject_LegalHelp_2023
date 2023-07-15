namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;


    public class LegalAdviseController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ILegalAdviseService legalAdviseService;

        public LegalAdviseController(ITicketService _ticketService, ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService)
        {
            this.ticketService = _ticketService;
            this.legalAdvisorService = _legalAdvisorService;
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
                this.TempData[ErrorMessage] = "Ticket with the provided ticketId does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(ticketId);
            if (resolvedStatus == true)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided ticketId was already resolved!";
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
                legalAdviseFormModel.TicketSubject = await this.ticketService.GetTicketSubjectAsync(ticketId);
                legalAdviseFormModel.TicketDescription = await this.ticketService.GetTicketDescription(ticketId);
                legalAdviseFormModel.TicketId = ticketId;

                return View(legalAdviseFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Add
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
                this.TempData[ErrorMessage] = "Ticket with the provided ticketId does not exist!";
                return this.RedirectToAction("All", "Ticket");
            }
            bool resolvedStatus = await this.ticketService
                .ResolvedTicket(model.TicketId);
            if (resolvedStatus == true)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided ticketId was already resolved!";
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

                await this.ticketService.AddLegalAdviseToTicketByIdAsync(model.TicketId, legalAdviseId);

                await this.legalAdvisorService.AddLegalAdviseToLegalAdvisorByIdAsync(legalAdvisorId!, legalAdviseId);



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

        //Mine - just listing
        public async Task<IActionResult> Mine()
        {
            bool isLegalAdvisor =
                   await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "You must be a legal advisor or login as such in order to view yours legal advises!";

                return this.RedirectToAction("Become", "LegalAdvisor");
            }
            string? legalAdvisorId = await this.legalAdvisorService.GetLegalAdvisorIdByUserIdAsync(this.User.GetId()!);
            IEnumerable<LegalAdviseViewModel> model = await legalAdviseService.GetMyLegalAdvisesAsync(legalAdvisorId!);
            return View(model);
        }

        public async Task<IActionResult> Received()
        {
            List<LegalAdviseViewModel> myReceivedLegalAdvises =
                  new List<LegalAdviseViewModel>();

            string userId = this.User.GetId()!;

            try
            {
                myReceivedLegalAdvises.AddRange(await this.legalAdviseService.AllByUserIdAsync(userId!));

                return this.View(myReceivedLegalAdvises);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        
    }
}
