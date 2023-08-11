namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ReviewAdminService : IReviewAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public ReviewAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task DeleteTheReviewItSelfByDocumentIdAsync(string id)
        {
            Review review = await this.dbContext
                     .Reviews
                     .FirstOrDefaultAsync(x => x.DocumentId.ToString() == id);

            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                    .Reviews
                    .FirstOrDefaultAsync(x => x.LegalAdviseId.ToString() == id);
            }
        }
        public async Task DeleteSingleReviewItSelfByDocumentIdAsync(string objectId, string textReview)
        {
            Review review = await this.dbContext
                 .Reviews
                 .Where(x => x.TextReview == textReview)
                 .FirstOrDefaultAsync(x => x.DocumentId.ToString() == objectId);

            //in case of duplicate of the same text review if we want to delete the one we will want to delete all which are the same
            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                 .Reviews
                 .Where(x => x.TextReview == textReview)
                 .FirstOrDefaultAsync(x => x.DocumentId.ToString() == objectId);
            }
        }

        public async Task DeleteTheReviewItSelfByLegalAdviseIdAsync(string id)
        {
            Review review = await this.dbContext
                    .Reviews
                    .FirstOrDefaultAsync(x => x.LegalAdviseId.ToString() == id);

            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                   .Reviews
                   .FirstOrDefaultAsync(x => x.LegalAdviseId.ToString() == id);
            }
        }

        public async Task DeleteSingleReviewItSelfByLegalAdviseIdAsync(string objectId, string textReview)
        {
            Review review = await this.dbContext
                   .Reviews
                   .Where(x => x.TextReview == textReview)
                   .FirstOrDefaultAsync(x => x.LegalAdviseId.ToString() == objectId);
            //in case of duplicate of the same text review if we want to delete the one we will want to delete all which are the same
            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                   .Reviews
                   .Where(x => x.TextReview == textReview)
                   .FirstOrDefaultAsync(x => x.LegalAdviseId.ToString() == objectId);
            }
        }

        public async Task DeleteTheReviewItSelfByLegalAdvisorIdAsync(string legalAdvisorUserId)
        {
            Review review = await this.dbContext
                   .Reviews
                   .FirstOrDefaultAsync(x => x.LegalAdvisor.UserId.ToString() == legalAdvisorUserId);

            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                   .Reviews
                   .FirstOrDefaultAsync(x => x.LegalAdvisor.UserId.ToString() == legalAdvisorUserId);
            }
        }

        public async Task DeleteSingleReviewItSelfByLegalAdvisorIdAsync(string objectId, string textReview)
        {
            Review review = await this.dbContext
                            .Reviews
                            .Where(x => x.TextReview == textReview)
                            .FirstOrDefaultAsync(x => x.LegalAdvisor.UserId.ToString() == objectId);

            //in case of duplicate of the same text review if we want to delete the one we will want to delete all which are the same
            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                            .Reviews
                            .Where(x => x.TextReview == textReview)
                            .FirstOrDefaultAsync(x => x.LegalAdvisor.UserId.ToString() == objectId);
            }
        }

        public async Task DeleteTheReviewItSelfByUploaderIdAsync(string uploaderUserId)
        {
            Review review = await this.dbContext
                   .Reviews
                   .FirstOrDefaultAsync(x => x.Uploader.UserId.ToString() == uploaderUserId);

            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                   .Reviews
                   .FirstOrDefaultAsync(x => x.Uploader.UserId.ToString() == uploaderUserId);
            }
        }
        public async Task DeleteSingleReviewItSelfByUploaderIdAsync(string objectId, string textReview)
        {
            Review review = await this.dbContext
                  .Reviews
                  .Where(x => x.TextReview == textReview)
                  .FirstOrDefaultAsync(x => x.Uploader.UserId.ToString() == objectId);

            //in case of duplicate of the same text review if we want to delete the one we will want to delete all which are the same
            while (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
                review = await this.dbContext
                  .Reviews
                  .Where(x => x.TextReview == textReview)
                  .FirstOrDefaultAsync(x => x.Uploader.UserId.ToString() == objectId);
            }
        }

    }
}
