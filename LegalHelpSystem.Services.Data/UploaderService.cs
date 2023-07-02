namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Web.ViewModels.Uploader;

    public class UploaderService : IUploaderService
    {
        private readonly LegalHelpDbContext dbContext;

        public UploaderService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> UploaderExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Uploaders
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeUploaderFormModel model)
        {
            Uploader newUploader = new Uploader()
            {
                AuthorRightsDescription = model.AuthorRightsDescription,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Uploaders.AddAsync(newUploader);
            await this.dbContext.SaveChangesAsync();
        }


    }
}
