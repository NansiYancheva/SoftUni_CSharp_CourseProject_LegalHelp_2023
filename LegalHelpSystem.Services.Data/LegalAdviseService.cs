namespace LegalHelpSystem.Services.Data
{
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using Microsoft.EntityFrameworkCore;

    public class LegalAdviseService : ILegalAdviseService
    {
        private readonly LegalHelpDbContext dbContext;

        public LegalAdviseService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add Legal Advise-Post
        public async Task<string> AddLegalAdviseAsync(LegalAdviseFormModel formModel, string legalAdvisorId)
        {
            LegalAdvise legalAdvise = new LegalAdvise
            {
                AdviseResponse = formModel.AdviseResponse,
                TicketId = Guid.Parse(formModel.TicketId),
                //Ticket = ?
                LegalAdvisorId = Guid.Parse(legalAdvisorId)
                //LegalAdvisor = ?
            };

            await this.dbContext.LegalAdvises.AddAsync(legalAdvise);
            await this.dbContext.SaveChangesAsync();

            return legalAdvise.Id.ToString();
        }

        //Add LegalAdviseToTicket
        public async Task AddLegalAdviseToTicketByIdAsync(string ticketId, TicketForAnswerFormModel model)
        {
            LegalAdvise legalAdvise = new LegalAdvise
            {
                //AdviseResponse = model.Response.AdviseResponse,
                TicketId = Guid.Parse(ticketId),
            };

            await dbContext.LegalAdvises.AddAsync(legalAdvise);
            await dbContext.SaveChangesAsync();
        }

        //Mine
        public async Task<IEnumerable<LegalAdviseViewModel>> AllByLegalAdvisorIdAsync(string legalAdvisorId)
        {
            IEnumerable<LegalAdviseViewModel> allLegalAdvisorAdvises = await this.dbContext
                .LegalAdvises
                .Include(h => h.LegalAdvisor)
                .Include(h => h.Ticket)
                .Where(h => h.LegalAdvisorId.ToString() == legalAdvisorId)
                .Select(h => new LegalAdviseViewModel
                {
                    Id = h.Id.ToString(),
                    AdviseResponse = h.AdviseResponse,
                    TicketId = h.Ticket.Id,
                    Ticket = h.Ticket,
                    LegalAdvisor = h.LegalAdvisor
                })
                .ToArrayAsync();

            return allLegalAdvisorAdvises;
        }


    }
}
