namespace LegalHelpSystem.Web.Areas.Admin.Services
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

        public async Task RemoveLegalAdviseFromTicket(string ticketId)
        {
            Ticket ticket = await this.dbContext
              .Tickets
              .FirstOrDefaultAsync(x => x.Id.ToString() == ticketId);

            ticket.LegalAdviseId = null;
            ticket.Response = null;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string> GetTicketIdBDocumentIdAsync(string id)
        {
            Document document = await this.dbContext
               .Documents
               .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            return document.TicketId.ToString();
        }

        public async Task RemoveDocumentFromTicket(string ticketId)
        {
            Ticket ticket = await this.dbContext
              .Tickets
              .FirstOrDefaultAsync(x => x.Id.ToString() == ticketId);

            ticket.DocumentId = null;
            ticket.Document = null;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task ChangeTicketStatusAsync(string ticketId)
        {
            Ticket ticket = await this.dbContext
              .Tickets
              .FirstOrDefaultAsync(x => x.Id.ToString() == ticketId);

            ticket.ResolvedTicketStatus = false;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
