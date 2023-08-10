namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface IDocumentAdminService
    {
        Task DeleteDocumentByIdAsync(string id);
        Task<string> FindDocumentIdByTicketIdAsync(string id);
        Task RemoveReviewsOfDocumentAsync(string id);
    }
}
