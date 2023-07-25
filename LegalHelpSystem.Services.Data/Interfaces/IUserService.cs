using LegalHelpSystem.Web.ViewModels.User;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId);
        Task<IEnumerable<AllTeamMembersViewModel>> GetAllTeamMembers();
    }
}
