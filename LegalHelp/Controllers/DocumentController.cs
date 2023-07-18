namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;



    public class DocumentController : BaseController
    {
        private readonly IDocumentService documentService;
        private readonly IUploaderService uploaderService;
        private readonly ITicketService ticketService;
        private readonly IDocumentTypeService documentTypeService;


        public DocumentController(IDocumentService _documentService, IUploaderService _uploaderService, ITicketService _ticketService, IDocumentTypeService _documentTypeService)
        {
            this.documentService = _documentService;
            this.uploaderService = _uploaderService;
            this.ticketService = _ticketService;
            this.documentTypeService = _documentTypeService;
        }

        //All - get
        public async Task<IActionResult> All()
        {
            IEnumerable<DocumentAllViewModel> model = await documentService.GetAllDocumentsAsync();
            return View(model);
        }
        //Mine - just listing
        public async Task<IActionResult> Mine()
        {
            bool isUploader =
                   await this.uploaderService.UploaderExistsByUserIdAsync(this.User.GetId()!);
            if (!isUploader)
            {
                this.TempData[ErrorMessage] = "You must be an uploader or login as such in order to view your uploaded documents!";

                return this.RedirectToAction("Become", "Uploader");
            }
            string? uploaderId = await this.uploaderService.GetUploaderIdByUserIdAsync(this.User.GetId()!);
            IEnumerable<DocumentAllViewModel> model = await documentService.GetMyUploadedDocumentsAsync(uploaderId!);
            return View(model);
        }

        //Downloaded
        public async Task<IActionResult> Downloaded()
        {
            IEnumerable<DocumentAllViewModel> model = await documentService.GetDownloadedByUserAsync(this.User.GetId()!);
            return View(model);
        }
        //Upload
        [HttpGet]
        public async Task<IActionResult> Upload(string ticketId)
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
            bool isUploader =
                await this.uploaderService.UploaderExistsByUserIdAsync(this.User.GetId()!);
            if (!isUploader)
            {
                this.TempData[ErrorMessage] = "You must become an uploader in order to upload legal document!";

                return this.RedirectToAction("Become", "Uploader");
            }

            try
            {
                DocumentFormModel documentFormModel = new DocumentFormModel()
                {
                    Types = await this.documentTypeService.AllDocumentTypesAsync()
                };
                documentFormModel.TicketSubject = await this.ticketService.GetTicketSubjectAsync(ticketId);
                documentFormModel.TicketDescription = await this.ticketService.GetTicketDescription(ticketId);
                documentFormModel.TicketId = ticketId;

                return View(documentFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Upload
        [HttpPost]
        public async Task<IActionResult> Upload(DocumentFormModel model, IFormFile documentFile)
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
            bool isUploader =
                await this.uploaderService.UploaderExistsByUserIdAsync(this.User.GetId()!);
            if (!isUploader)
            {
                this.TempData[ErrorMessage] = "You must become an uploader in order to upload legal document!";

                return this.RedirectToAction("Become", "Uploader");
            }
            bool typesExists =
                await this.documentTypeService.ExistsByIdAsync(model.DocumentTypeId);
            if (!typesExists)
            {
                this.ModelState.AddModelError(nameof(model.DocumentTypeId), "Selected category does not exist!");
            }

            try
            {
                if (documentFile != null && documentFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        documentFile.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        string? uploaderId = await this.uploaderService.GetUploaderIdByUserIdAsync(this.User.GetId()!);

                        string documentId = await this.documentService.UploadDocumentAsync(model, uploaderId!, fileBytes);

                        await this.ticketService.AddDocumentToTicketByIdAsync(model.TicketId, documentId);

                        await this.uploaderService.AddDocumentToUploaderByIdAsync(uploaderId!, documentId);
                    }
                }
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Document was added successfully!";

            return this.RedirectToAction("All", "Ticket");
        }
        //Download
        [HttpGet]
        public async Task<IActionResult> Download(string ticketId)
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
            if (!resolvedStatus)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided ticketId is not resolved and there is no document for download!";
                return this.RedirectToAction("All", "Ticket");
            }
            //no documentId in the ticket?will this be possible once the status is resolved?

            //is this possible?
            if (this.User.GetId() == null)
            {
                this.TempData[ErrorMessage] = "You must become an user of the website in order to download legal document!";

                return this.RedirectToAction("Register", "Home");
            }

            try
            {
                DocumentForDownloadViewModel documentFrDownloadModel = await this.documentService
                   .GetDocumentForDownload(ticketId);

                if (documentFrDownloadModel == null)
                {
                    this.TempData[ErrorMessage] = "There is no document for download!";
                    return this.RedirectToAction("All", "Ticket");
                }
                string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; 
                string fileName = "document_file.docx"; 

                return File(documentFrDownloadModel.DocumentFile, contentType, fileName);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

    }
}
