namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    public class TicketByNoUserViewModel
    {
        public string Subject { get; set; } = null!;

        public string TicketCategory { get; set; } = null!;

        public string RequestDescription { get; set; } = null!;

        public bool ResolvedTicketStatus { get; set; }
    }
}
