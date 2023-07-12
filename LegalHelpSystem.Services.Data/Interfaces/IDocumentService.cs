namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    public interface IDocumentService
    {
        //All - get
        Task<IEnumerable<DocumentAllViewModel>> GetAllDocumentsAsync();


        //Mine - just listing
        Task<IEnumerable<DocumentAllViewModel>> GetMyUploadedDocumentsAsync(string uploaderId);

        //Downloaded
        Task<IEnumerable<DocumentAllViewModel>> GetDownloadedByUserAsync(string userId);
    }
}
