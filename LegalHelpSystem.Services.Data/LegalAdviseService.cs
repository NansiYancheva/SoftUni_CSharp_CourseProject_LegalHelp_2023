namespace LegalHelpSystem.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
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
                //AdviseResponse = model.AdviseResponse,
                TicketId = Guid.Parse(ticketId),
            };

            await dbContext.LegalAdvises.AddAsync(legalAdvise);
            await dbContext.SaveChangesAsync();
        }
        //List Received by User Legal Advises
        public async Task<IEnumerable<LegalAdviseViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<LegalAdviseViewModel> allUserReceivedLegalAdvises = await this.dbContext
                .LegalAdvises
                .Include(la => la.Ticket)
                .Where(la => la.Ticket.UserId.ToString().ToLower() == userId)
                .Select(la => new LegalAdviseViewModel
                {
                    TicketSubject = la.Ticket.Subject,
                    TicketDescription = la.Ticket.RequestDescription,
                    AdviseResponse = la.AdviseResponse,
                }).ToListAsync();

            return allUserReceivedLegalAdvises;
        }
        //All by everybody
        public async Task<IEnumerable<LegalAdviseViewModel>> GetAllLegalAdvisesAsync()
        {
            return await this.dbContext
                 .LegalAdvises
                 .Include(x => x.Ticket)
                 .Include(x => x.LegalAdvisor)
                 .Where(x => x.Ticket.Subject != null)
                 .Select(h => new LegalAdviseViewModel
                 {
                     TicketSubject = h.Ticket.Subject,
                     TicketDescription = h.Ticket.RequestDescription,
                     AdviseResponse = h.AdviseResponse
                 })
                 .ToListAsync();
        }

        //Mine(legal advisor)
        public async Task<IEnumerable<LegalAdviseViewModel>> GetMyLegalAdvisesAsync(string legalAdvisorId)
        {
            IEnumerable<LegalAdviseViewModel> listOfLegalAdvises = 
                 await dbContext.LegalAdvises
                .Where(la => la.LegalAdvisorId.ToString() == legalAdvisorId && la.Ticket.RequestDescription != null)
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
