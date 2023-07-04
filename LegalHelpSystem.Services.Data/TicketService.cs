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
