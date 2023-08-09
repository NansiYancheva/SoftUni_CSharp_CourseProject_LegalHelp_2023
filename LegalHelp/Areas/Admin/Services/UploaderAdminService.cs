﻿namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class UploaderAdminService : IUploaderAdminService
    {
        private readonly LegalHelpDbContext dbContext;

        public UploaderAdminService(LegalHelpDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task RemoveReviewsOfUploaderAsync(string uploaderUserId)
        {
            Uploader uploader = await this.dbContext
              .Uploaders
              .Include(x => x.Reviews)
              .FirstAsync(x => x.UserId.ToString() == uploaderUserId);

            uploader.Reviews.Clear();

            await this.dbContext.SaveChangesAsync();
        }
    }
}
