

namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserAdminService : IUserAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public UserAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateUploader(string userId)
        {
            Uploader newUploader = new Uploader()
            {
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Uploaders.AddAsync(newUploader);
            await this.dbContext.SaveChangesAsync();
        }
        public async Task UnmakeUploader(string uploaderUserId)
        {
            Uploader existingUploader = await this.dbContext
              .Uploaders
              .FirstOrDefaultAsync(x => x.UserId.ToString() == uploaderUserId);

            this.dbContext.Uploaders.Remove(existingUploader);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task CreateLegalAdvisor(string userId)
        {
            LegalAdvisor newLegalAdvisor = new LegalAdvisor()
            {
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.LegalAdvisors.AddAsync(newLegalAdvisor);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnmakeLegalAdvisor(string legalAdvisorUserId)
        {
            LegalAdvisor existingLegalAdvisor = await this.dbContext
                .LegalAdvisors
                .FirstOrDefaultAsync(x => x.UserId.ToString() == legalAdvisorUserId);

            this.dbContext.LegalAdvisors.Remove(existingLegalAdvisor);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
