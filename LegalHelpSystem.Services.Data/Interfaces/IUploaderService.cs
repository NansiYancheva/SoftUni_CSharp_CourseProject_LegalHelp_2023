namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Uploader;
    public interface IUploaderService
    {
        Task Create(string userId, BecomeUploaderFormModel model);

        //Common
        Task<bool> UploaderExistsByUserIdAsync(string userId);

        Task<string?> GetUploaderIdByUserIdAsync(string userId);


    }
}
