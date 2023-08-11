namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class LegalAdvisorAdminService : ILegalAdvisorAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public LegalAdvisorAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> ChooseLegalAdvisorUserIdAsync()
        {
            LegalAdvisor choosenLegalAdvisor = await this.dbContext
            .LegalAdvisors
            .FirstAsync(h => h.Id.ToString() != null);

            return choosenLegalAdvisor.Id.ToString();
        }

        public async Task RemoveReviewsOfLegalAdvisorAsync(string legalAdvisorUserId)
        {
            LegalAdvisor legalAdvisor = await this.dbContext
              .LegalAdvisors
              .Include(x => x.Reviews)
              .FirstAsync(x => x.UserId.ToString() == legalAdvisorUserId);

            legalAdvisor.Reviews.Clear();

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveSingleReviewFromListOfReviewsOfLegalAdvisorAsync(string objectId, string textReview)
        {
            LegalAdvisor legalAdvisor = await this.dbContext
              .LegalAdvisors
              .Include(x => x.Reviews)
              .FirstAsync(x => x.UserId.ToString() == objectId);

            Review reviewToBeRemoved = legalAdvisor.Reviews.Where(x => x.TextReview == textReview).FirstOrDefault();

            while (reviewToBeRemoved != null)
            {
                legalAdvisor.Reviews.Remove(reviewToBeRemoved);
                await this.dbContext.SaveChangesAsync();
                reviewToBeRemoved = legalAdvisor.Reviews.Where(x => x.TextReview == textReview).FirstOrDefault();
            }
        }
    }
}
