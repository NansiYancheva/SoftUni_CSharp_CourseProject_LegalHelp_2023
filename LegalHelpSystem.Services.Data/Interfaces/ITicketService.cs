namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Ticket;

    public interface ITicketService
    {
        //Add-Post
        Task<string> AddTicketAsync(TicketAddOrEditFormModel model, string userId);

        //Edit-Get
        Task<TicketAddOrEditFormModel> GetTicketForEditByIdAsync(string ticketId);
        //Edit-Post
        Task EditTicketByIdAndFormModelAsync(string ticketId, TicketAddOrEditFormModel formModel);

        //Delete-Get
        Task<TicketPerDeleteFormModel> GetTicketForDeleteByIdAsync(string ticketId);
        //Delete-Post
        Task DeleteTicketByIdAsync(string ticketId);

        //Mine
        Task<IEnumerable<TicketAllViewModel>> AllByUserIdAsync(string userId);

        //All-user
        Task<IEnumerable<TicketAllViewModel>> GetAllTicketsAsync();

        //Add LegalAdvise To Ticket
        Task AddLegalAdviseToTicketByIdAsync(string ticketId, string legalAdviseId);

        //Common
        Task<bool> ExistsByIdAsync(string ticketId);
        Task<bool> IsUserReporterOfTheTicket(string ticketId, string userId);
        Task<bool> ResolvedTicket(string ticketId);
        Task<string> GetTicketSubjectAsync(string ticketId);
        Task<string> GetTicketDescription(string ticketId);

    }
}
