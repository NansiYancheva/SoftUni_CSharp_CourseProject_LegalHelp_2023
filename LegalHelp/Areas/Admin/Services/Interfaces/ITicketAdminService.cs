namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface ITicketAdminService
    {
        Task<string> GetTicketIdByLegalAdviseIdAsync(string id);
        Task RemoveLegalAdviseFromTicket(string ticketId);

        Task<string> GetTicketIdBDocumentIdAsync(string id);

        Task RemoveDocumentFromTicket(string ticketId);
        Task ChangeTicketStatusAsync(string ticketId);
    }
}
