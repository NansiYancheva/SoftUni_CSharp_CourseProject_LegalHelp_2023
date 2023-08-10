namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Ticket;


    public class TicketService : ITicketService
    {
        private readonly LegalHelpDbContext dbContext;

        public TicketService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add-post
        public async Task<string> AddTicketAsync(TicketAddOrEditFormModel formModel, string userId)
        {
            Ticket ticket = new Ticket
            {
                Subject = formModel.Subject,
                TicketCategoryId = formModel.TicketCategoryId,
                RequestDescription = formModel.RequestDescription,
                UserId = Guid.Parse(userId)
            };

            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();

            return ticket.Id.ToString();
        }

        //Edit-get
        public async Task<TicketAddOrEditFormModel> GetTicketForEditByIdAsync(string ticketId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .Include(h => h.TicketCategory)
                .FirstAsync(h => h.Id.ToString() == ticketId);

            return new TicketAddOrEditFormModel
            {
                Subject = ticket.Subject,
                TicketCategoryId = ticket.TicketCategoryId,
                RequestDescription = ticket.RequestDescription
            };
        }

        //Edit-post
        public async Task EditTicketByIdAndFormModelAsync(string ticketId, TicketAddOrEditFormModel formModel)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .FirstAsync(h => h.Id.ToString() == ticketId);

            ticket.Subject = formModel.Subject;
            ticket.RequestDescription = formModel.RequestDescription;
            ticket.TicketCategoryId = formModel.TicketCategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        //Delete - get
        public async Task<TicketPerDeleteFormModel> GetTicketForDeleteByIdAsync(string ticketId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .Include(h => h.Response)
                .Include(h => h.Document)
                .FirstAsync(h => h.Id.ToString() == ticketId);

            TicketPerDeleteFormModel ticketViewModel = 
             new TicketPerDeleteFormModel ()
            {
                Subject = ticket.Subject,
                RequestDescription = ticket.RequestDescription,
                ResolvedTicketStatus = ticket.ResolvedTicketStatus,
            };

            if(ticket.Document != null)
            {
                ticketViewModel.DocumentName = ticket.Document.Name;
            }
            if (ticket.Response != null)
            {
                ticketViewModel.Response = ticket.Response.AdviseResponse;
            }

            return ticketViewModel;
        }
        //Delte-post
        public async Task DeleteTicketByIdAsync(string ticketId)
        {
            Ticket ticketToDelete = await this.dbContext
                .Tickets
                .FirstAsync(h => h.Id.ToString() == ticketId);
           

            this.dbContext.Remove(ticketToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        //Mine
        public async Task<IEnumerable<TicketAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<TicketAllViewModel> allUserTickets = await this.dbContext
                .Tickets
                .Include(h => h.TicketCategory)
                .Include(h => h.Document.Uploader)
                .ThenInclude(h => h.User)
                .Include(h => h.Response.LegalAdvisor)
                .ThenInclude(h => h.User)
                .Where(h => h.UserId.ToString() == userId)
                .Select(h => new TicketAllViewModel
                {
                    Id = h.Id.ToString(),
                    Subject = h.Subject,
                    ResolvedTicketStatus = h.ResolvedTicketStatus,
                    TicketCategory = h.TicketCategory.Name,
                    RequestDescription = h.RequestDescription,
                    LegalAdviseId = h.LegalAdviseId,
                    LegalAdvisorName = $"{h.Response.LegalAdvisor.User.FirstName} {h.Response.LegalAdvisor.User.LastName}",
                    LegalAdvisorUserId = h.Response.LegalAdvisor.User.Id.ToString(),
                    Response = h.Response.AdviseResponse,
                   DocumentId = h.DocumentId,
                    UploaderName = $"{h.Document.Uploader.User.FirstName} {h.Document.Uploader.User.LastName}",
                    UploaderUserId = h.Document.Uploader.User.Id.ToString(),
                })
                .ToArrayAsync(); 

            return allUserTickets;
        }
        //All
        public async Task<IEnumerable<TicketAllViewModel>> GetAllTicketsAsync()
        {
            List <TicketAllViewModel> tickets =  await this.dbContext
                .Tickets
                .Include(h => h.TicketCategory)
                .Include(h => h.Document.Uploader)
                .ThenInclude(h => h.User)
                .Include(h => h.Response.LegalAdvisor)
                .ThenInclude(h => h.User)
                .Select(h => new TicketAllViewModel
                {
                    Id = h.Id.ToString(),
                    Subject = h.Subject,
                    TicketCategory = h.TicketCategory.Name,
                    RequestDescription = h.RequestDescription,
                    ResolvedTicketStatus = h.ResolvedTicketStatus,
                    LegalAdviseId = h.LegalAdviseId,
                    LegalAdvisorName = $"{h.Response.LegalAdvisor.User.FirstName} {h.Response.LegalAdvisor.User.LastName}",
                    LegalAdvisorUserId = h.Response.LegalAdvisor.User.Id.ToString(),
                    Response = h.Response.AdviseResponse,
                    DocumentId = h.DocumentId,
                    UploaderName = $"{h.Document.Uploader.User.FirstName} {h.Document.Uploader.User.LastName}",
                    UploaderUserId = h.Document.Uploader.User.Id.ToString(),

                })
                .ToListAsync();

            return tickets;
        }

        //Add Legal Advise to Ticket
        public async  Task AddLegalAdviseToTicketByIdAsync(string ticketId, string legalAdviseId)
        {
            Ticket ticketToBeUpdated = await this.dbContext
               .Tickets
               .FirstAsync(t => t.Id.ToString() == ticketId);

            LegalAdvise legalAdviseToBeAddedToTicket = await this.dbContext
               .LegalAdvises
               .FirstAsync(la => la.Id.ToString() == legalAdviseId);

            ticketToBeUpdated.LegalAdviseId = legalAdviseToBeAddedToTicket.Id;
            ticketToBeUpdated.ResolvedTicketStatus = true;

            await this.dbContext.SaveChangesAsync();
        }
        //Add Document to Ticket
        public async Task AddDocumentToTicketByIdAsync(string ticketId, string documentId)
        {
            Ticket ticketToBeUpdated = await this.dbContext
               .Tickets
               .FirstAsync(t => t.Id.ToString() == ticketId);

            Document documentToBeAddedToTicket = await this.dbContext
               .Documents
               .FirstAsync(d => d.Id.ToString() == documentId);

            ticketToBeUpdated.DocumentId= documentToBeAddedToTicket.Id;
            ticketToBeUpdated.ResolvedTicketStatus = true;

            await this.dbContext.SaveChangesAsync();
        }


        //Common
        public async Task<bool> ExistsByIdAsync(string ticketId)
        {
            bool result = await this.dbContext
                .Tickets
                .AnyAsync(t => t.Id.ToString() == ticketId);
            return result;
        }
        public async Task<bool> IsUserReporterOfTheTicket(string ticketId, string userId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .FirstAsync(h => h.Id.ToString() == ticketId);

            return ticket.UserId.ToString() == userId;
        }
        public async Task<bool> ResolvedTicket(string ticketId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .FirstAsync(h => h.Id.ToString() == ticketId);
            return ticket.ResolvedTicketStatus == true;
        }
        public async Task<string> GetTicketSubjectAsync(string ticketId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                 .FirstOrDefaultAsync(h => h.Id.ToString() == ticketId);

            return ticket.Subject;
        }
        public async Task<string> GetTicketDescription(string ticketId)
        {
            Ticket ticket = await this.dbContext
                .Tickets
                .FirstOrDefaultAsync(h => h.Id.ToString() == ticketId);

            return ticket.RequestDescription;
        }


    }
}
