namespace LegalHelpSystem.Web.Areas.Admin.Services
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;


    public class UserAdminService : IUserAdminService
    {
        private readonly LegalHelpDbContext dbContext;
        private readonly ILegalAdviseAdminService legalAdviseAdminService;
        private readonly ITicketAdminService ticketAdminService;
        private readonly ILegalAdvisorAdminService legalAdvisorAdminService;
        private readonly IDocumentAdminService documentAdminService;
        private readonly IReviewAdminService reviewAdminService;
        private readonly IUploaderAdminService uploaderAdminService;

        public UserAdminService(LegalHelpDbContext dbContext, ITicketAdminService _ticketAdminService, ILegalAdviseAdminService _legalAdviseAdminService, IReviewAdminService _reviewAdminService, ILegalAdvisorAdminService _legalAdvisorAdminService, IDocumentAdminService _documentAdminService, IUploaderAdminService _uploaderAdminService)
        {
            this.dbContext = dbContext;       
            this.ticketAdminService = _ticketAdminService;          
            this.legalAdviseAdminService = _legalAdviseAdminService;
            this.reviewAdminService = _reviewAdminService;
            this.legalAdvisorAdminService = _legalAdvisorAdminService;
            this.documentAdminService = _documentAdminService;
            this.uploaderAdminService = _uploaderAdminService;
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
               .Include(x => x.UploadedDocuments)
              .Include(x => x.Reviews)
              .FirstOrDefaultAsync(x => x.UserId.ToString() == uploaderUserId);


            foreach (var document in existingUploader.UploadedDocuments)
            {
                string id = document.Id.ToString();
                //find ticket to which the document is given
                string ticketId = await this.ticketAdminService
                   .GetTicketIdBDocumentIdAsync(id);
                //first remove document from ticket
                await this.ticketAdminService
                    .RemoveDocumentFromTicket(ticketId);
                //remove the reviews
                await this.documentAdminService.RemoveReviewsOfDocumentAsync(id);
                //change ticket status to not resolved
                await this.ticketAdminService.ChangeTicketStatusAsync(ticketId);
                //delete the review by document id
                await this.reviewAdminService.DeleteTheReviewItSelfByDocumentIdAsync(id);
                //After that delete the document itself
                await this.documentAdminService.DeleteDocumentByIdAsync(id);
            }
            //delete all reviews of the uploader
            await this.uploaderAdminService.RemoveReviewsOfUploaderAsync(existingUploader.UserId.ToString());
            //delete the review itself
            await this.reviewAdminService.DeleteTheReviewItSelfByUploaderIdAsync(existingUploader.UserId.ToString());

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
                .Include(x => x.LegalAdvises)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.UserId.ToString() == legalAdvisorUserId);


            foreach (var legalAdvise in existingLegalAdvisor.LegalAdvises)
            {
                string id = legalAdvise.Id.ToString();
                //find ticket to which the legal advise is given
                string ticketId = await this.ticketAdminService
                  .GetTicketIdByLegalAdviseIdAsync(id);
                //first remove legalAdviseIdFromTicket
                await this.ticketAdminService
                    .RemoveLegalAdviseFromTicket(ticketId);
                //remove the reviews
                await this.legalAdviseAdminService.RemoveReviewsOfLegalAdviseAsync(id);
                //change ticket status to not resolved
                await this.ticketAdminService.ChangeTicketStatusAsync(ticketId);
                //delete the review itself by legal advise id
                await this.reviewAdminService.DeleteTheReviewItSelfByLegalAdviseIdAsync(id);
                //After that delete the legalAdvise itself
                await this.legalAdviseAdminService.DeleteLegalAdviseByIdAsync(id);
            }
            //delete all reviews of the legal advisor
            await this.legalAdvisorAdminService.RemoveReviewsOfLegalAdvisorAsync(existingLegalAdvisor.UserId.ToString());
            //delete the review itself
            await this.reviewAdminService.DeleteTheReviewItSelfByLegalAdvisorIdAsync(existingLegalAdvisor.UserId.ToString());
            this.dbContext.LegalAdvisors.Remove(existingLegalAdvisor);
            await this.dbContext.SaveChangesAsync();
        }

       

    }
}
