﻿namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using Microsoft.EntityFrameworkCore;

    public class LegalAdviseAdminService : ILegalAdviseAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public LegalAdviseAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetLegalAdviseAsResponse(string id)
        {
            LegalAdvise legalAdvise = await this.dbContext
                .LegalAdvises
                .FirstAsync(h => h.Id.ToString() == id);

            return legalAdvise.AdviseResponse;
        }

        public async Task EditLegalAdviseByIdAndFormModelAsync(string id, LegalAdviseFormModel model)
        {
            LegalAdvise legalAdvise = await this.dbContext
                .LegalAdvises
                .FirstAsync(h => h.Id.ToString() == id);

            legalAdvise.AdviseResponse = model.AdviseResponse;

            await this.dbContext.SaveChangesAsync();
        }


    }
}
