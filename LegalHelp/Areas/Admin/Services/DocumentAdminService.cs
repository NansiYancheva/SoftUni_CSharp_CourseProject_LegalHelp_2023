namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;


    public class DocumentAdminService : IDocumentAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public DocumentAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteDocumentByIdAsync(string id)
        {
            Document documentToDelete = await this.dbContext
                    .Documents
                    .FirstAsync(h => h.Id.ToString() == id);

            this.dbContext.Documents.Remove(documentToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string> FindDocumentIdByTicketIdAsync(string id)
        {
            Document document = await this.dbContext
                    .Documents
                    .FirstAsync(x => x.TicketId.ToString() == id);

            return document.Id.ToString();
        }

        public async Task RemoveReviewsOfDocumentAsync(string id)
        {
            Document document = await this.dbContext
                     .Documents
                     .Include(x => x.Reviews)
                     .FirstAsync(h => h.Id.ToString() == id);

            document.Reviews.Clear();

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveSingleReviewFromListOfReviewsOfDocumentAsync(string objectId, string textReview)
        {
            Document document = await this.dbContext
             .Documents
             .Include(x => x.Reviews)
             .FirstAsync(x => x.Id.ToString() == objectId);

            Review reviewToBeRemoved = document.Reviews.Where(x => x.TextReview == textReview).FirstOrDefault();

            while (reviewToBeRemoved != null)
            {
                document.Reviews.Remove(reviewToBeRemoved);
                await this.dbContext.SaveChangesAsync();
                reviewToBeRemoved = document.Reviews.Where(x => x.TextReview == textReview).FirstOrDefault();
            }
        }
    }
}
