namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;

    using static Common.NotificationMessagesConstants;


    public class LegalAdviseAdminController : BaseAdminController
    {
        private readonly ILegalAdviseService legalAdviseService;
        private readonly ILegalAdviseAdminService legalAdviseAdminService;
        private readonly ITicketAdminService ticketAdminService;
        private readonly ITicketService ticketService;
        private readonly ITicketCategoryService ticketCategoryService;



        public LegalAdviseAdminController(ILegalAdviseService _legalAdviseService, ITicketAdminService _ticketAdminService, ITicketService _ticketService, ITicketCategoryService _ticketCategoryService, ILegalAdviseAdminService _legalAdviseAdminService)
        {
            this.legalAdviseService = _legalAdviseService;
            this.ticketAdminService = _ticketAdminService;
            this.ticketService = _ticketService;
            this.ticketCategoryService = _ticketCategoryService;
            this.legalAdviseAdminService = _legalAdviseAdminService;
        }
        //??
        [Route("LegalAdviseAdmin/EditLegalAdvise")]
        //Edit
        [HttpGet]
        public async Task<IActionResult> EditLegalAdvise(string id)
        {
            bool legalAdviseExists = await this.legalAdviseService
                .LegalAdviseExistsByIdAsync(id);
            if (!legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "Legal Advise with the provided id does not exist!";

                return this.RedirectToAction("All", "LegalAdvise");
            }
            try
            {
                string ticketId = await this.ticketAdminService
                    .GetTicketIdByLegalAdviseIdAsync(id);
                LegalAdviseFormModel legalAdviseFormModel = new LegalAdviseFormModel();
                legalAdviseFormModel.TicketSubject = await this.ticketService
                    .GetTicketSubjectAsync(ticketId);
                legalAdviseFormModel.TicketDescription = await this.ticketService
                    .GetTicketDescription(ticketId);
                legalAdviseFormModel.TicketId = ticketId;
                legalAdviseFormModel.AdviseResponse = await this.legalAdviseAdminService
                    .GetLegalAdviseAsResponse(id);
                legalAdviseFormModel.Id = id;

                return View(legalAdviseFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }


        //Edit
        [Route("LegalAdviseAdmin/EditLegalAdvise")]
        [HttpPost]
        public async Task<IActionResult> EditLegalAdvise(string id, LegalAdviseFormModel model)
        { 
            bool legalAdviseExists = await this.legalAdviseService
               .LegalAdviseExistsByIdAsync(id);
            if (!legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "Legal Advise with the provided id does not exist!";

                return this.RedirectToAction("All", "LegalAdvise", new { Area = "" });
            }
            try
            {
                await this.legalAdviseAdminService.EditLegalAdviseByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the legal advise. Please try again later!");
                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Legal Advise was edited successfully!";
            return this.RedirectToAction("All", "LegalAdvise", new { Area = "" });
        }

        ////Delete
        //[HttpGet]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    bool ticketExists = await this.ticketService
        //        .ExistsByIdAsync(id);
        //    if (!ticketExists)
        //    {
        //        this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

        //        return this.RedirectToAction("Mine", "Ticket");
        //    }

        //    string? userId = this.User.GetId();
        //    bool isReporter = await this.ticketService
        //        .IsUserReporterOfTheTicket(id, userId!);
        //    if (!isReporter && !this.User.IsAdmin())
        //    {
        //        this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

        //        return this.RedirectToAction("Mine", "Ticket");
        //    }

        //    try
        //    {
        //        TicketPerDeleteFormModel viewModel =
        //            await this.ticketService.GetTicketForDeleteByIdAsync(id);

        //        return this.View(viewModel);
        //    }
        //    catch (Exception)
        //    {
        //        return this.GeneralError();
        //    }
        //}
        ////Delete        
        //[HttpPost]
        //public async Task<IActionResult> Delete(string id, TicketPerDeleteFormModel model)
        //{
        //    bool ticketExists = await this.ticketService
        //        .ExistsByIdAsync(id);
        //    if (!ticketExists)
        //    {
        //        this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

        //        return this.RedirectToAction("Mine", "Ticket");
        //    }

        //    string? userId = this.User.GetId();
        //    bool isReporter = await this.ticketService
        //        .IsUserReporterOfTheTicket(id, userId!);
        //    if (!isReporter && !this.User.IsAdmin())
        //    {
        //        this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

        //        return this.RedirectToAction("Mine", "Ticket");
        //    }
        //    try
        //    {
        //        await this.ticketService.DeleteTicketByIdAsync(id);

        //        this.TempData[WarningMessage] = "The ticket was successfully deleted!";
        //        return this.RedirectToAction("Mine", "Ticket");
        //    }
        //    catch (Exception)
        //    {
        //        return this.GeneralError();
        //    }
        //}
    }
}
