﻿namespace LegalHelpSystem.Services.Data
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
                .FirstAsync(h => h.Id.ToString() == ticketId);

            return new TicketPerDeleteFormModel
            {
                Subject = ticket.Subject,
                RequestDescription = ticket.RequestDescription,
                ResolvedTicketStatus = ticket.ResolvedTicketStatus,
                LegalAdviseId = ticket.LegalAdviseId
            };
        }
        //Delte-post
        public async Task DeleteTicketByIdAsync(string ticketId)
        {
            Ticket ticketToDelete = await this.dbContext
                .Tickets
                .FirstAsync(h => h.Id.ToString() == ticketId);
            
            //is this because of delete restrictions?
            //ticketToDelete.IsActive = false;

            this.dbContext.Remove(ticketToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        //Mine
        public async Task<IEnumerable<TicketAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<TicketAllViewModel> allUserTickets = await this.dbContext
                .Tickets
                .Include(h => h.TicketCategory)
                .Include(h => h.Response)
                .Where(h => h.UserId.ToString() == userId)
                .Select(h => new TicketAllViewModel
                {
                    Id = h.Id.ToString(),
                    Subject = h.Subject,
                    TicketCategory = h.TicketCategory.Name,
                    RequestDescription = h.RequestDescription,
                    Response = h.Response.AdviseResponse
                })
                .ToArrayAsync(); 

            return allUserTickets;
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
