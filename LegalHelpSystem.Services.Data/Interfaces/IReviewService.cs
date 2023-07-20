namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Review;
    public interface IReviewService
    {
        Task AddLegalAdviseReview(ReviewFormModel model, string userId);
    }
}
