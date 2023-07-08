namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;

    public class LegalAdvisorService : ILegalAdvisorService
    {
        private readonly LegalHelpDbContext dbContext;

        public LegalAdvisorService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> LegalAdvisorExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .LegalAdvisors
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

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
    }
}
