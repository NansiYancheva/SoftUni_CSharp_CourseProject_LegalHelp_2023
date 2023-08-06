namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IDocumentAdminService
    {
        Task DeleteDocumentByIdAsync(string id);
        Task RemoveReviewsOfDocumentAsync(string id);
    }
}
