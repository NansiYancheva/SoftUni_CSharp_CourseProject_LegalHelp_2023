namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    public interface IDocumentService
    {
        //All - get
        Task<IEnumerable<DocumentAllViewModel>> GetAllDocumentsAsync();
    }
}
