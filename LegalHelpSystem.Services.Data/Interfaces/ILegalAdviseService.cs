using LegalHelpSystem.Web.ViewModels.Ticket;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface ILegalAdviseService
    {
        //Add-post
        Task AddLegalAdviseToTicketByIdAsync(string ticketId, TicketForAnswerFormModel model);
    }
}
