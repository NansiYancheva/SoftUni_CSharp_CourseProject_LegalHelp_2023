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
    }
}
