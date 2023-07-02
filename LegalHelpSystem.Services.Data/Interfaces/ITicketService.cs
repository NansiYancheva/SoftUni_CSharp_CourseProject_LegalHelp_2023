using LegalHelpSystem.Web.ViewModels.Ticket;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface ITicketService
    {
        //Add-Post
        Task AddTicketAsync(TicketAddOrEditFormModel model);
    }
}
