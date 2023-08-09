namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IUserAdminService
    {
        //Become Uploader
        Task CreateUploader(string userId);

        Task CreateLegalAdvisor(string userId);
        Task UnmakeUploader(string uploaderUserId);
        Task UnmakeLegalAdvisor(string legalAdvisorUserId);
    }
}
