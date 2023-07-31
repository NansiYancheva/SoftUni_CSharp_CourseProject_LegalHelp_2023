using LegalHelpSystem.Web.ViewModels.User;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFullNameByEmailAsync(string email);

        Task<string> GetFullNameByIdAsync(string userId);
        Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId);
        Task<IEnumerable<AllTeamMembersViewModel>> GetAllTeamMembers();

        Task<IEnumerable<AllTeamMembersViewModel>> GetAllUsers();


    }
}
