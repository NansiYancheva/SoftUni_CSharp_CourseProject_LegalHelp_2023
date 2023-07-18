using LegalHelpSystem.Web.ViewModels.LegalAdvise;
using LegalHelpSystem.Web.ViewModels.LegalAdvisor;
using LegalHelpSystem.Web.ViewModels.Ticket;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface ILegalAdviseService
    {

        //Add legal advise
        Task<string> AddLegalAdviseAsync(LegalAdviseFormModel formModel, string legalAdvisorId);
        //Add Legal Advise To Ticket
        Task AddLegalAdviseToTicketByIdAsync(string ticketId, TicketForAnswerFormModel model);
        //List Received by User Legal Advises
        Task<IEnumerable<LegalAdviseViewModel>> AllByUserIdAsync(string userId);

        //Mine(legal advisor) 
        Task<IEnumerable<LegalAdviseViewModel>> GetMyLegalAdvisesAsync(string legalAdvisorId);
        //All by everybody
        Task<IEnumerable<LegalAdviseViewModel>> GetAllLegalAdvisesAsync();

    }
}
