namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface ITicketAdminService
    {
        Task<string> GetTicketIdByLegalAdviseIdAsync(string id);
        Task RemoveLegalAdviseFromTicket(string ticketId);
    }
}
