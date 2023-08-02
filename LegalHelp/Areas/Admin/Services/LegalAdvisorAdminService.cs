﻿namespace LegalHelpSystem.Web.Areas.Admin.Services
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
    }
}