namespace LegalHelpSystem.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;
    using LegalHelpSystem.Web.ViewModels.Ticket;


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

        //Mine - just listing
        public async Task<IEnumerable<LegalAdviseViewModel>> GetMyLegalAdvisesAsync(string legalAdvisorId)
        {
            var listOfLegalAdvises = 
                 await dbContext.LegalAdvises
                .Where(la => la.LegalAdvisorId.ToString() == legalAdvisorId)
                .Select(la => new LegalAdviseViewModel
                {
                    TicketSubject = la.Ticket.Subject,
                    TicketDescription = la.Ticket.RequestDescription,
                    AdviseResponse = la.AdviseResponse,
                }).ToListAsync();

            return listOfLegalAdvises;
        }


    }
}
