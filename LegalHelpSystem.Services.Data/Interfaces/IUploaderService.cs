namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Review;
    using LegalHelpSystem.Web.ViewModels.Uploader;
    public interface IUploaderService
    {
        ////Add Document To Uploader
        Task AddDocumentToUploaderByIdAsync(string uploaderId, string documentId);

        //Common
        Task<bool> UploaderExistsByUserIdAsync(string userId);

        Task<string?> GetUploaderIdByUserIdAsync(string userId);

        Task<string> GetUploaderNameAsync(string objectId);

        Task<ReviewsViewModel> GetUploaderReviews(string id);
    }
}
