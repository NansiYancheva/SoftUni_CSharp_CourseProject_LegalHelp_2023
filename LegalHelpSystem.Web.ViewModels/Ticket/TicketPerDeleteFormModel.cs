namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using LegalHelpSystem.Data.Models;

    public class TicketPerDeleteFormModel
    {
        public string Subject { get; set; } = null!;

        public string RequestDescription { get; set; } = null!;

        public bool ResolvedTicketStatus { get; set; }

        public Guid? LegalAdviseId { get; set; }
        public virtual LegalAdvise? Response { get; set; }
    }
}
