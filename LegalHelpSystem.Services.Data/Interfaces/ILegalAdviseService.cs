using LegalHelpSystem.Web.ViewModels.LegalAdvise;
using LegalHelpSystem.Web.ViewModels.Ticket;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface ILegalAdviseService
    {

        //Add legal advise
        Task<string> AddLegalAdviseAsync(LegalAdviseFormModel formModel, string userId);
        Task AddLegalAdviseToTicketByIdAsync(string ticketId, TicketForAnswerFormModel model);
    }
}
