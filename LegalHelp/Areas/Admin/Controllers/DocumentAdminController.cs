namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Web.ViewModels.LegalAdvise;

    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Data.Models;

    public class DocumentAdminController : BaseAdminController
    {
        private readonly IDocumentService documentService;
        private readonly IDocumentAdminService documentAdminService;
        private readonly ITicketAdminService ticketAdminService;
        private readonly IReviewAdminService reviewAdminService;
        private readonly ITicketService ticketService;

        public DocumentAdminController(IDocumentService _documentService, ITicketAdminService ticketAdminService, ITicketService _ticketService, IDocumentAdminService _documentAdminService, IReviewAdminService _reviewAdminService)
        {
            this.documentService = _documentService;
            this.ticketAdminService = ticketAdminService;
            this.ticketService = _ticketService;
            this.documentAdminService = _documentAdminService;
            this.reviewAdminService = _reviewAdminService;
        }

        //Delete
        [Route("DocumentAdmin/DeleteDocument")]
        [HttpGet]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            bool documentExist = await this.documentService
             .DocumentExistsByIdAsync(id);
            if (!documentExist)
            {
                this.TempData[ErrorMessage] = "Document with the provided id does not exist!";

                return this.RedirectToAction("All", "Document", new { Area = "" });
            }

            try
            {
                string ticketId = await this.ticketAdminService
                    .GetTicketIdBDocumentIdAsync(id);
                DocumentFormModel documentFormModel = new DocumentFormModel
                {
                    TicketSubject = await this.ticketService
                    .GetTicketSubjectAsync(ticketId),
                    TicketDescription = await this.ticketService
                    .GetTicketDescription(ticketId),
                    TicketId = ticketId,
                    DocumentName = await this.documentService
             .GetDocumentNameAsync(id),
                    Id = id
                };

                return this.View(documentFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Delete   
        [Route("DocumentAdmin/DeleteDocument")]
        [HttpPost]
        public async Task<IActionResult> DeleteDocument(string id, DocumentFormModel model)
        {
            bool documentExist = await this.documentService
             .DocumentExistsByIdAsync(id);
            if (!documentExist)
            {
                this.TempData[ErrorMessage] = "Document with the provided id does not exist!";

                return this.RedirectToAction("All", "Document", new { Area = "" });
            }
            try
            {
                string ticketId = await this.ticketAdminService
                   .GetTicketIdBDocumentIdAsync(id);
                //first remove document from ticket
                await this.ticketAdminService
                    .RemoveDocumentFromTicket(ticketId);
                //remove the reviews
                await this.documentAdminService.RemoveReviewsOfDocumentAsync(id);
                //change ticket status to not resolved
                await this.ticketAdminService.ChangeTicketStatusAsync(ticketId);
                //delete the review by document id
                await this.reviewAdminService.DeleteTheReviewItSelfByDocumentIdAsync(id);
                //After that delete the legalAdvise itself
                await this.documentAdminService.DeleteDocumentByIdAsync(id);
              

                //what will happen with the reviews?

                this.TempData[WarningMessage] = "The document was successfully deleted!";
                return this.RedirectToAction("All", "Document", new { Area = "" });
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
      

    }
}
