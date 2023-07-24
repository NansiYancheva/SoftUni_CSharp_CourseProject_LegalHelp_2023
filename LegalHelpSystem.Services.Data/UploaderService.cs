namespace LegalHelpSystem.Services.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Web.ViewModels.Uploader;
    using LegalHelpSystem.Web.ViewModels.Review;



    public class UploaderService : IUploaderService
    {
        private readonly LegalHelpDbContext dbContext;

        public UploaderService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Become
        public async Task Create(string userId, BecomeUploaderFormModel model)
        {
            Uploader newUploader = new Uploader()
            {
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Uploaders.AddAsync(newUploader);
            await this.dbContext.SaveChangesAsync();
        }

        //Add Document To Uploader
        public async Task AddDocumentToUploaderByIdAsync(string uploaderId, string documentId)
        {
            Document documentToBeAddedToUploader = await this.dbContext
               .Documents
               .FirstAsync(d => d.Id.ToString() == documentId);

            Uploader currentUploader = await this.dbContext
               .Uploaders
               .FirstAsync(u => u.Id.ToString() == uploaderId);

            currentUploader.UploadedDocuments.Add(documentToBeAddedToUploader);
            await this.dbContext.SaveChangesAsync();
        }

        //Common
        public async Task<bool> UploaderExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Uploaders
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }
        public async Task<string?> GetUploaderIdByUserIdAsync(string userId)
        {
            Uploader? uploader = await this.dbContext
                .Uploaders
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (uploader == null)
            {
                return null;
            }

            return uploader.Id.ToString();
        }

        public async Task<string> GetUploaderNameAsync(string objectId)
        {
            Uploader uploader = await this.dbContext
               .Uploaders
               .Include(x => x.User)
               .FirstOrDefaultAsync(a => a.UserId.ToString() == objectId);

            string uploaderName = $"{uploader.User.FirstName} {uploader.User.LastName}";

            return uploaderName;
        }

        public async Task<ReviewsViewModel> GetUploaderReviews(string id)
        {
            Uploader uploader = await this.dbContext
            .Uploaders
            .Include(x => x.Reviews)
            .Include(x => x.User)
             .FirstOrDefaultAsync(a => a.UserId.ToString() == id);

            List<string> listOfTextReviews = uploader.Reviews
                .Select(x => x.TextReview)
                .ToList();

            int totalStars = uploader.Reviews
                .Select(x => x.Stars)
                .Sum();

            int aggTotalStars;
            if (totalStars == 0)
            {
                aggTotalStars = 0;
            }
            else
            {
                aggTotalStars = totalStars / uploader.Reviews.Count;
            }

            return new ReviewsViewModel
            {
                Object = $"{uploader.User.FirstName} {uploader.User.LastName}",
                TextReviews = listOfTextReviews,
                TotalStars = aggTotalStars
            };
        }
    }
}
