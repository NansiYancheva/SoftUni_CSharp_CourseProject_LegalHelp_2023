namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;
    using LegalHelpSystem.Web.ViewModels.Review;

    public class LegalAdvisorService : ILegalAdvisorService
    {
        private readonly LegalHelpDbContext dbContext;

        public LegalAdvisorService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Become
        public async Task Create(string userId, BecomeLegalAdvisorFormModel model)
        {
            LegalAdvisor newLegalAdvisor = new LegalAdvisor()
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.LegalAdvisors.AddAsync(newLegalAdvisor);
            await this.dbContext.SaveChangesAsync();
        }

        //Add LegalAdvise To LegalAdvisor
        public async Task AddLegalAdviseToLegalAdvisorByIdAsync(string legalAdvisorId, string legalAdviseId)
        {
            LegalAdvise legalAdviseToBeAddedToLegalAdvisor = await this.dbContext
               .LegalAdvises
               .FirstAsync(h => h.Id.ToString() == legalAdviseId);

            LegalAdvisor currentLegalAdvisor = await this.dbContext
               .LegalAdvisors
               .FirstAsync(h => h.Id.ToString() == legalAdvisorId);

            currentLegalAdvisor.LegalAdvises.Add(legalAdviseToBeAddedToLegalAdvisor);
            await this.dbContext.SaveChangesAsync();
        }

        //Common
        public async Task<bool> LegalAdvisorExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .LegalAdvisors
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }
        public async Task<string?> GetLegalAdvisorIdByUserIdAsync(string userId)
        {
            LegalAdvisor? legalAdvisor = await this.dbContext
                .LegalAdvisors
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (legalAdvisor == null)
            {
                return null;
            }

            return legalAdvisor.Id.ToString();
        }

        public async Task<string> GetLegalAdvisorNameAsync(string objectId)
        {
            LegalAdvisor legalAdvisor = await this.dbContext
             .LegalAdvisors
             .FirstOrDefaultAsync(a => a.UserId.ToString() == objectId);

            return legalAdvisor.User.UserName;
        }

        public async Task<ReviewsViewModel> GetLegalAdvisorReviews(string id)
        {
            LegalAdvisor legalAdvisor = await this.dbContext
          .LegalAdvisors
          .Include(x => x.Reviews)
           .FirstOrDefaultAsync(a => a.UserId.ToString() == id);

            List<string> listOfTextReviews = legalAdvisor.Reviews
                .Select(x => x.TextReview)
                .ToList();

            int totalStars = legalAdvisor.Reviews
                .Select(x => x.Stars)
                .Sum();

            int aggTotalStars;
            if (totalStars == 0)
            {
                aggTotalStars = 0;
            }
            else
            {
                aggTotalStars = totalStars / legalAdvisor.Reviews.Count;
            }

            return new ReviewsViewModel
            {
                Object = $"{legalAdvisor.User.FirstName} {legalAdvisor.User.LastName}",
                TextReviews = listOfTextReviews,
                TotalStars = aggTotalStars
            };
        }
    }
}
