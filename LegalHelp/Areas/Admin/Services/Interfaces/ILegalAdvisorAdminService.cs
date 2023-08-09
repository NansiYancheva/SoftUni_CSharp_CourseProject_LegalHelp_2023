namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface ILegalAdvisorAdminService
    {
        Task<string> ChooseLegalAdvisorUserIdAsync();
        Task RemoveReviewsOfLegalAdvisorAsync(string legalAdvisorUserId);
    }
}
