namespace LegalHelpSystem.Services.Data
{
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
        public async Task AddTicketAsync(TicketAddOrEditFormModel model)
        {
            Ticket ticket = new Ticket
            {
               Subject = model.Subject,
               TicketCategoryId = model.TicketCategoryId,
               RequestDescription = model.RequestDescription
            };

            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
        }
    }
}
