namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Document;
    public interface IApplicationUserService
    {
        Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId);
    }
}
