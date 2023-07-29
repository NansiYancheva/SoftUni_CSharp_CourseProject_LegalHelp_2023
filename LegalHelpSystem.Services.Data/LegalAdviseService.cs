namespace LegalHelpSystem.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using LegalHelpSystem.Web.ViewModels.Review;

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

        //List Received by User Legal Advises
        public async Task<IEnumerable<LegalAdviseViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<LegalAdviseViewModel> allUserReceivedLegalAdvises = await this.dbContext
                .LegalAdvises
                .Include(la => la.Ticket)
                .Include(la => la.LegalAdvisor)
                .ThenInclude(la => la.User)
                .Where(la => la.Ticket.UserId.ToString().ToLower() == userId)
                .Select(la => new LegalAdviseViewModel
                {
                    Id = la.Id.ToString(),
                    TicketSubject = la.Ticket.Subject,
                    TicketDescription = la.Ticket.RequestDescription,
                    AdviseResponse = la.AdviseResponse,
                    LegalAdvisorName = $"{la.LegalAdvisor.User.FirstName}  {la.LegalAdvisor.User.LastName}",
                    LegalAdvisorUserId = la.LegalAdvisor.UserId.ToString()
                }).ToListAsync();

            return allUserReceivedLegalAdvises;
        }
        //All by everybody
        public async Task<IEnumerable<LegalAdviseViewModel>> GetAllLegalAdvisesAsync()
        {
            List<LegalAdviseViewModel> listOfLegalAdvises =  await this.dbContext
                 .LegalAdvises
                 .Include(x => x.Ticket)
                 .Include(x => x.LegalAdvisor)
                 .ThenInclude(x => x.User)
                 .Where(x => x.Ticket.Subject != null)
                 .Select(h => new LegalAdviseViewModel
                 {
                     Id = h.Id.ToString(),
                     TicketSubject = h.Ticket.Subject,
                     TicketDescription = h.Ticket.RequestDescription,
                     AdviseResponse = h.AdviseResponse,
                     LegalAdvisorName = $"{h.LegalAdvisor.User.FirstName}  {h.LegalAdvisor.User.LastName}",
                     LegalAdvisorUserId = h.LegalAdvisor.UserId.ToString()
                 })
                 .ToListAsync();

            return listOfLegalAdvises;
        }



        //Mine(legal advisor)
        public async Task<IEnumerable<LegalAdviseViewModel>> GetMyLegalAdvisesAsync(string legalAdvisorId)
        {
            LegalAdvisor legalAdvisor = await this.dbContext
               .LegalAdvisors
               .Include(x => x.User)
               .Include(x => x.LegalAdvises)
               .ThenInclude(x => x.Ticket)
               .FirstAsync(x => x.Id.ToString() == legalAdvisorId);

            List<LegalAdvise> legalAdvisorLegalAdvises = legalAdvisor
                .LegalAdvises
                .Where(x => x.Ticket != null)
                .ToList();

            List<LegalAdviseViewModel> allGivenLAByLegalAdvisor = new List<LegalAdviseViewModel>();

            foreach (LegalAdvise legalAdvice in legalAdvisorLegalAdvises)
            {
                LegalAdviseViewModel currLA = new LegalAdviseViewModel
                {
                    Id = legalAdvice.Id.ToString(),
                    AdviseResponse = legalAdvice.AdviseResponse,
                    TicketSubject = legalAdvice.Ticket.Subject,
                    TicketDescription = legalAdvice.Ticket.RequestDescription,
                    LegalAdvisorName = $"{legalAdvice.LegalAdvisor.User.FirstName}  {legalAdvice.LegalAdvisor.User.LastName}",
                    LegalAdvisorUserId = legalAdvice.LegalAdvisor.UserId.ToString()
                };
                allGivenLAByLegalAdvisor.Add(currLA);
            }

            return allGivenLAByLegalAdvisor;
        }

        //Common
        public async Task<bool> LegalAdviseExistsByIdAsync(string objectId)
        {
            bool result = await this.dbContext
               .LegalAdvises
               .AnyAsync(a => a.Id.ToString() == objectId);

            return result;
        }

        public async Task<string> GetLegalAdviseResponseAsync(string objectId)
        {
            LegalAdvise legalAdvise = await this.dbContext
                .LegalAdvises
                .FirstOrDefaultAsync(x => x.Id.ToString() == objectId);

            return legalAdvise.AdviseResponse;
        }

        public async Task<ReviewsViewModel> GetLegalAdviseReviews(string id)
        {
            LegalAdvise legalAdvise = await this.dbContext
          .LegalAdvises
          .Include(x => x.Reviews)
           .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            List<string> listOfTextReviews = legalAdvise.Reviews
                .Select(x => x.TextReview)
                .ToList();

            int totalStars = legalAdvise.Reviews
                .Select(x => x.Stars)
                .Sum();

            int aggTotalStars;
            if (totalStars == 0)
            {
                aggTotalStars = 0;
            }
            else
            {
                aggTotalStars = totalStars / legalAdvise.Reviews.Count;
            }

            return new ReviewsViewModel
            {
                Object = legalAdvise.AdviseResponse,
                TextReviews = listOfTextReviews,
                TotalStars = aggTotalStars
            };
        }
    }
}
