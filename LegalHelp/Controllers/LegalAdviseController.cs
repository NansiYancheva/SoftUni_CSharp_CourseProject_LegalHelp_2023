namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;

    using static Common.NotificationMessagesConstants;
    

    public class LegalAdviseController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ILegalAdviseService legalAdviseService;
        private readonly ILegalAdvisorAdminService legalAdvisorAdminService;

        public LegalAdviseController(ITicketService _ticketService, ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService, ILegalAdvisorAdminService _legalAdvisorAdminService)
        {
            this.ticketService = _ticketService;
            this.legalAdvisorService = _legalAdvisorService;
            this.legalAdviseService = _legalAdviseService;
            this.legalAdvisorAdminService = _legalAdvisorAdminService;
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
                await this.legalAdvisorService
                .LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be a legal advisor in order to answer legal question!";

                return this.RedirectToAction("All", "Ticket");
            }

            try
            {
                LegalAdviseFormModel legalAdviseFormModel = new LegalAdviseFormModel();
                legalAdviseFormModel.TicketSubject = await this.ticketService
                    .GetTicketSubjectAsync(ticketId);
                legalAdviseFormModel.TicketDescription = await this.ticketService
                    .GetTicketDescription(ticketId);
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
                await this.legalAdvisorService
                .LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be a legal advisor in order to answer legal question!";

                return this.RedirectToAction("All", "Ticket");
            }
            try
            {
                //
                string legalAdvisorId;

                if (isLegalAdvisor)
                {
                    legalAdvisorId = await this.legalAdvisorService
                    .GetLegalAdvisorIdByUserIdAsync(this.User.GetId()!);
                }
                //if an admin will add legal advisor there should be a legal advisor Id in order to add the legal advise
                //this wont be a practise but just in case an admin should add a legal advise
                else
                {
                    legalAdvisorId = await this.legalAdvisorAdminService
                        .ChooseLegalAdvisorUserIdAsync();
                }
                //
                string legalAdviseId = await legalAdviseService
                    .AddLegalAdviseAsync(model, legalAdvisorId!);
                //
                await this.ticketService
                    .AddLegalAdviseToTicketByIdAsync(model.TicketId, legalAdviseId);

                await this.legalAdvisorService
                    .AddLegalAdviseToLegalAdvisorByIdAsync(legalAdvisorId!, legalAdviseId);



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

        //All - get
        public async Task<IActionResult> All()
        {
            IEnumerable<LegalAdviseViewModel> model = await legalAdviseService
                .GetAllLegalAdvisesAsync();
            return View(model);
        }

        //All given by legal advisor
        public async Task<IActionResult> Given()
        {
            List<LegalAdviseViewModel> legalAdvisorGivenLegalAdvises =
              new List<LegalAdviseViewModel>();

            bool isLegalAdvisor =
                   await this.legalAdvisorService
                   .LegalAdvisorExistsByUserIdAsync(this.User.GetId()!);
            if (!isLegalAdvisor)
            {
                this.TempData[ErrorMessage] = "You must be a legal advisor or login as such in order to view yours legal advises!";

                return this.RedirectToAction("Become", "LegalAdvisor");
            }
            try
            {
                string? legalAdvisorId = await this.legalAdvisorService
                    .GetLegalAdvisorIdByUserIdAsync(this.User.GetId()!);
                legalAdvisorGivenLegalAdvises.AddRange(await legalAdviseService
                    .GetMyLegalAdvisesAsync(legalAdvisorId!));
                return this.View(legalAdvisorGivenLegalAdvises);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        //Mine - received
        public async Task<IActionResult> Received()
        {
            List<LegalAdviseViewModel> myReceivedLegalAdvises =
                  new List<LegalAdviseViewModel>();

            string userId = this.User.GetId()!;

            try
            {
                if (User.IsAdmin())
                {
                    return RedirectToAction("All", "LegalAdvise");
                }
                else
                {
                    myReceivedLegalAdvises.AddRange(await this.legalAdviseService.AllByUserIdAsync(userId!));
                }

                return this.View(myReceivedLegalAdvises);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        
    }
}
