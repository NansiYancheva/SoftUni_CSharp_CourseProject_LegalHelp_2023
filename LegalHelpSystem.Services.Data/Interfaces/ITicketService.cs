namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        //Add-Post
        Task<string> AddTicketAsync(TicketAddOrEditFormModel model, string userId);

        //Common
        Task<bool> ExistsByIdAsync(string ticketId);
        Task<bool> IsUserReporterOfTheTicket(string ticketId, string userId);
        Task<bool> ResolvedTicket(string ticketId);

    }
}
