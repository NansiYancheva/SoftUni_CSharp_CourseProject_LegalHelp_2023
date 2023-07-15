namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Uploader;
    public interface IUploaderService
    {
        //Become Uploader
        Task Create(string userId, BecomeUploaderFormModel model);
        ////Add Document To Uploader
        Task AddDocumentToUploaderByIdAsync(string uploaderId, string documentId);

        //Common
        Task<bool> UploaderExistsByUserIdAsync(string userId);

        Task<string?> GetUploaderIdByUserIdAsync(string userId);

    }
}
