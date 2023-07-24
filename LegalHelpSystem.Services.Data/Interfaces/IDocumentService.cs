namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Web.ViewModels.Review;

    public interface IDocumentService
    {
        //All - get
        Task<IEnumerable<DocumentAllViewModel>> GetAllDocumentsAsync();


        //Mine - just listing
        Task<IEnumerable<DocumentAllViewModel>> GetMyUploadedDocumentsAsync(string uploaderId);

        //Downloaded
        Task<IEnumerable<DocumentAllViewModel>> GetDownloadedByUserAsync(string userId);

        //Add Document-Post
        Task<string> UploadDocumentAsync(DocumentFormModel model, string uploaderId, byte[] fileBytes);

        //download document
        Task<DocumentForDownloadViewModel> GetDocumentForDownload(string ticketId);

        //Common
        Task<bool> DocumentExistsByIdAsync(string objectId);
        Task<string> GetDocumentNameAsync(string objectId);
        Task<ReviewsViewModel> GetDocumentReviews(string id);
    }
}
