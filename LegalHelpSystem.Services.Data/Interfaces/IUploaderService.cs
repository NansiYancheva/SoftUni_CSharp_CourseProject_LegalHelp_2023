namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Uploader;
    public interface IUploaderService
    {
        Task<bool> UploaderExistsByUserIdAsync(string userId);

        Task Create(string userId, BecomeUploaderFormModel model);


    }
}
