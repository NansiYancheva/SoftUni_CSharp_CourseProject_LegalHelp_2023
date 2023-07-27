namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Review;
    using System.Collections.Generic;

    public class ReviewService : IReviewService
    {
        private readonly LegalHelpDbContext dbContext;

        public ReviewService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddLegalAdviseReview(ReviewFormModel model, string userId)
        {
            //to create the review itself in the database
            Review newReview = new Review()
            {
                TextReview = model.TextReview,
                Stars = model.Stars,
                //LegalAdviseId = model.LegalAdviseId,
                //LegalAdvise = model.LegalAdvise,
                UserId = Guid.Parse(userId)

            };

            await this.dbContext.Reviews.AddAsync(newReview);

            //to add review to the object
            LegalAdvise existingLegalAdvise = await this.dbContext
                 .LegalAdvises
                 .Include(x => x.Reviews)
                 .FirstOrDefaultAsync(x => x.Id == Guid.Parse(model.ObjectId));

            existingLegalAdvise.Reviews.Add(newReview);

            //to add review to User list of reviews
            ApplicationUser user = await this.dbContext
                .Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            user.Reviews.Add(newReview);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddUploaderReview(ReviewFormModel model, string userId)
        {
            //to create the review itself in the database
            Review newReview = new Review()
            {
                TextReview = model.TextReview,
                Stars = model.Stars,
                //UploaderId = model.UploaderId,
                //Uploader = model.Uploader,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Reviews.AddAsync(newReview);

            //to add review to the object
            Uploader existingUploader = await this.dbContext
                 .Uploaders
                 .Include(x => x.Reviews)
                 .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(model.ObjectId));

            existingUploader.Reviews.Add(newReview);

            //to add review to User list of reviews
            ApplicationUser user = await this.dbContext
                .Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            user.Reviews.Add(newReview);

            await this.dbContext.SaveChangesAsync();
        }
        public async Task AddLegalAdvisorReview(ReviewFormModel model, string userId)
        {
            //to create the review itself in the database
            Review newReview = new Review()
            {
                TextReview = model.TextReview,
                Stars = model.Stars,
                //LegalAdvisorId = model.LegalAdvisorId,
                //LegalAdvisor = model.LegalAdvisor,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Reviews.AddAsync(newReview);

            //to add review to the object
            LegalAdvisor existingLegalAdvisor = await this.dbContext
                 .LegalAdvisors
                 .Include(x => x.Reviews)
                 .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(model.ObjectId));

            existingLegalAdvisor.Reviews.Add(newReview);

            //to add review to User list of reviews
            ApplicationUser user = await this.dbContext
                .Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            user.Reviews.Add(newReview);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task AddDocumentReview(ReviewFormModel model, string userId)
        {
            //to create the review itself in the database
            Review newReview = new Review()
            {
                TextReview = model.TextReview,
                Stars = model.Stars,
                //DocumentId = model.DocumentId,
                //Document = model.Document,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Reviews.AddAsync(newReview);

            //to add review to the object
            Document existingDocument = await this.dbContext
                 .Documents
                 .Include(x => x.Reviews)
                 .FirstOrDefaultAsync(x => x.Id == Guid.Parse(model.ObjectId));

            existingDocument.Reviews.Add(newReview);

            //to add review to User list of reviews
            ApplicationUser user = await this.dbContext
                .Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            user.Reviews.Add(newReview);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
