namespace LegalHelpSystem.Services.Data
{
    using System.Threading.Tasks;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Data;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly LegalHelpDbContext dbContext;
        public ApplicationUserService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddDocToUserCollectionOfDocsAsync(string userId, string ticketId)
        {
           Document documentToBeAdded = await dbContext
                .Documents
                .FirstAsync(x => x.TicketId.ToString() == ticketId);

            //ApplicationUser userToBeUpdated = await this.dbContext
            //    .ApplicationUsers
            //    .FirstAsync(x => x.Id.ToString() == userId);


           //userToBeUpdated.DownloadedDocs.Add(documentToBeAdded);
           // documentToBeAdded.Downloaders.Add(userToBeUpdated);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
