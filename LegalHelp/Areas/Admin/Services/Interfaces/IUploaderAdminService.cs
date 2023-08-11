namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IUploaderAdminService
    {
        Task<string> ChooseUploaderUserIdAsync();
        Task RemoveReviewsOfUploaderAsync(string uploaderUserId);
        Task RemoveSingleReviewFromListOfReviewsOfUploaderAsync(string objectId, string textReview);
    }
}
