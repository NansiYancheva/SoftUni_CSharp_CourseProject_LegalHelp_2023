namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IUploaderAdminService
    {
        Task RemoveReviewsOfUploaderAsync(string uploaderUserId);
    }
}
