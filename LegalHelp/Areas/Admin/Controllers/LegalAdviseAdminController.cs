namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;

    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Data.Models;


    public class LegalAdviseAdminController : BaseAdminController
    {
        private readonly ILegalAdviseService legalAdviseService;
        private readonly ILegalAdviseAdminService legalAdviseAdminService;
        private readonly ITicketAdminService ticketAdminService;
        private readonly ITicketService ticketService;



        public LegalAdviseAdminController(ILegalAdviseService _legalAdviseService, ITicketAdminService _ticketAdminService, ITicketService _ticketService, ILegalAdviseAdminService _legalAdviseAdminService)
        {
            this.legalAdviseService = _legalAdviseService;
            this.ticketAdminService = _ticketAdminService;
            this.ticketService = _ticketService;

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

        //Delete
        [Route("LegalAdviseAdmin/DeleteLegalAdvise")]
        [HttpGet]
        public async Task<IActionResult> DeleteLegalAdvise(string id)
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

                return this.View(legalAdviseFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Delete   
        [Route("LegalAdviseAdmin/DeleteLegalAdvise")]
        [HttpPost]
        public async Task<IActionResult> DeleteLegalAdvise(string id, LegalAdviseFormModel model)
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
                string ticketId = await this.ticketAdminService
                   .GetTicketIdByLegalAdviseIdAsync(id);
                //first remove legalAdviseIdFromTicket
                await this.ticketAdminService
                    .RemoveLegalAdviseFromTicket(ticketId);
                //After that delete the legalAdvise itself
                await this.legalAdviseAdminService.DeleteLegalAdviseByIdAsync(id);
                //conflict - legalAdviseId in ticket

                //what will happen with the reviews?

                this.TempData[WarningMessage] = "The legal advise was successfully deleted!";
                return this.RedirectToAction("All", "LegalAdvise", new { Area = "" });
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
    }
}
