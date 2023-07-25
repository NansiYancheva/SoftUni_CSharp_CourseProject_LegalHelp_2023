namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using Interfaces;

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
    }
}
