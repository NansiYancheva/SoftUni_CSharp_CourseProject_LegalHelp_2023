namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;

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


    }
}
