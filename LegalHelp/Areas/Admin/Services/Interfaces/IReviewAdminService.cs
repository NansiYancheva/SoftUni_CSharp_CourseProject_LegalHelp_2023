namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IReviewAdminService
    {
        Task DeleteTheReviewItSelfByDocumentIdAsync(string id);
        Task DeleteTheReviewItSelfByLegalAdviseIdAsync(string id);
        Task DeleteTheReviewItSelfByLegalAdvisorIdAsync(string legalAdvisorUserId);
    }
}
