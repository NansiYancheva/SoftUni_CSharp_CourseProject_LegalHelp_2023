using LegalHelpSystem.Services.Data;
using LegalHelpSystem.Services.Data.Interfaces;
using LegalHelpSystem.Web.ViewModels.Ticket;
namespace LegalHelpSystem.Web.Controllers
{
    using LegalHelpSystem.Web.ViewModels.Document;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DocumentController : BaseController
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService _documentService)
        {
            this.documentService = _documentService;
        }

        [AllowAnonymous]
        //All - get
        public async Task<IActionResult> All()
        {
            IEnumerable<DocumentAllViewModel> model = await documentService.GetAllDocumentsAsync();
            return View(model);
        }
    }
}
