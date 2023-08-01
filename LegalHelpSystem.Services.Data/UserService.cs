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
        private readonly IUploaderService uploaderService;
        private readonly ILegalAdvisorService legalAdvisorService;

        public UserService(LegalHelpDbContext dbContext, IUploaderService uploaderService, ILegalAdvisorService legalAdvisorService)
        {
            this.dbContext = dbContext;
            this.uploaderService = uploaderService;
            this.legalAdvisorService = legalAdvisorService;
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

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
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
                    UploaderName = $"{uploader.User.FirstName} {uploader.User.LastName}",
                    UploaderUserId = uploader.UserId.ToString(),
                    UploaderReviews = await uploaderService.GetUploaderReviews(uploader.UserId.ToString()),
                    Email = uploader.User.Email
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
                    LegalAdvisorName = $"{legalAdvisor.User.FirstName} {legalAdvisor.User.LastName}",
                    LegalAdvisorUserId = legalAdvisor.UserId.ToString(),
                    LegalAdvisorReviews = await legalAdvisorService.GetLegalAdvisorReviews(legalAdvisor.UserId.ToString()),
                    Email = legalAdvisor.User.Email
                };
                allMembers.Add(currLegalAdvisor);
            }

            return allMembers;
        }



        public async Task<IEnumerable<AllTeamMembersViewModel>> GetAllUsers()
        {
            List<AllTeamMembersViewModel> allUsers = new List<AllTeamMembersViewModel>();
            allUsers.AddRange(await GetAllTeamMembers());
            List<ApplicationUser> allUsersInDb = await this.dbContext
                   .Users
                   .Include(x => x.Reviews)
                   .ToListAsync();

            foreach (ApplicationUser user in allUsersInDb)
            {

                AllTeamMembersViewModel currUser = new AllTeamMembersViewModel
                {
                    UserName = $"{user.FirstName} {user.LastName}",
                    UserId = user.Id.ToString(),
                    Email = user.Email
                };
                if (allUsers.Any(x => x.Email == currUser.Email))
                {
                    continue;
                }
                allUsers.Add(currUser);
            }
            return allUsers;
        }
    }
}
