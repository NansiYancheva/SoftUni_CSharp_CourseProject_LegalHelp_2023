﻿namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;


    public class TicketAdminService : ITicketAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public TicketAdminService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetTicketIdByLegalAdviseIdAsync(string id)
        {
            LegalAdvise legalAdvise = await this.dbContext
               .LegalAdvises
               .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            return legalAdvise.TicketId.ToString();
        }
    }
}
