namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using Interfaces;
    using LegalHelpSystem.Web.ViewModels.User;


    public class UserService : IUserService
    {
        private readonly LegalHelpDbContext dbContext;

        public UserService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId)
        {
            Document documentToBeAdded = await dbContext
                 .Documents
                 .FirstAsync(x => x.TicketId.ToString() == ticketId);

            ApplicationUser userToBeUpdated = await this.dbContext
                .Users
                .FirstAsync(x => x.Id.ToString() == userId);


            userToBeUpdated.DownloadedByUserDocs.Add(documentToBeAdded);


            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllTeamMembersViewModel>> GetAllTeamMembers()
        {
            List<AllTeamMembersViewModel> allMembers = new List<AllTeamMembersViewModel>();

            List<Uploader> allUploaders = await this.dbContext
                .Uploaders
                .Include(x => x.Reviews)
                .Include(x => x.User)
                .ToListAsync();
            foreach (Uploader uploader in allUploaders)
            {
                AllTeamMembersViewModel currUploader = new AllTeamMembersViewModel
                {
                    Uploader = uploader,
                    UploaderUserId = uploader.UserId.ToString(),
                    UploaderReviews = uploader.Reviews
                };
                allMembers.Add(currUploader);
            }

            List<LegalAdvisor> allLegalAdvisors = await this.dbContext
                    .LegalAdvisors
                    .Include(x => x.Reviews)
                    .Include(x => x.User)
                    .ToListAsync();
            foreach (LegalAdvisor legalAdvisor in allLegalAdvisors)
            {
                AllTeamMembersViewModel currLegalAdvisor = new AllTeamMembersViewModel
                {
                    LegalAdvisor = legalAdvisor,
                    LegalAdvisorUserId = legalAdvisor.UserId.ToString(),
                    LegalAdvisorReviews = legalAdvisor.Reviews
                };
                allMembers.Add(currLegalAdvisor);
            }

            return allMembers;
        }
    }
}
