namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;

    public interface ILegalAdviseAdminService
    {
        Task DeleteLegalAdviseByIdAsync(string id);
        Task EditLegalAdviseByIdAndFormModelAsync(string id, LegalAdviseFormModel model);
        Task<string> FindLegalAdviseIdByTicketIdAsync(string id);
        Task<string> GetLegalAdviseAsResponse(string id);
        Task RemoveReviewsOfLegalAdviseAsync(string id);
        Task RemoveSingleReviewFromListOfReviewsOfLegalAdviseAsync(string objectId, string textReview);
    }
}
