namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;

    public class DocumentController : BaseController
    {
        private readonly IDocumentService documentService;
        private readonly IUploaderService uploaderService;

        public DocumentController(IDocumentService _documentService, IUploaderService _uploaderService)
        {
            this.documentService = _documentService;
            this.uploaderService = _uploaderService;
        }

        [AllowAnonymous]
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
    }
}
