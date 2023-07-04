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
    }
}
