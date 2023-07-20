namespace LegalHelpSystem.Services.Data
{
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Review;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly LegalHelpDbContext dbContext;

        public ReviewService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddLegalAdviseReview(ReviewFormModel model, string userId)
        {
            Review newReview = new Review()
            {
                TextReview = model.TextReview,
                Stars = model.Stars,
                LegalAdviseId = model.LegalAdviseId,
                LegalAdvise = model.LegalAdvise,
                UserId = Guid.Parse(userId)

            };

            await this.dbContext.Reviews.AddAsync(newReview);

            LegalAdvise existingLegalAdvise = await this.dbContext
                 .LegalAdvises
                 .FirstOrDefaultAsync(x => x.Id == Guid.Parse(model.ObjectId));

            existingLegalAdvise.Reviews.Add(newReview);


            await this.dbContext.SaveChangesAsync();
        }
    }
}
