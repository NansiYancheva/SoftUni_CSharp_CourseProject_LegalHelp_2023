namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IReviewAdminService
    {
        Task DeleteSingleReviewItSelfByDocumentIdAsync(string objectId, string textReview);
        Task DeleteSingleReviewItSelfByLegalAdviseIdAsync(string objectId, string textReview);
        Task DeleteSingleReviewItSelfByLegalAdvisorIdAsync(string objectId, string textReview);
        Task DeleteSingleReviewItSelfByUploaderIdAsync(string objectId, string textReview);
        Task DeleteTheReviewItSelfByDocumentIdAsync(string id);
        Task DeleteTheReviewItSelfByLegalAdviseIdAsync(string id);
        Task DeleteTheReviewItSelfByLegalAdvisorIdAsync(string legalAdvisorUserId);
        Task DeleteTheReviewItSelfByUploaderIdAsync(string uploaderUserId);
    }
}
