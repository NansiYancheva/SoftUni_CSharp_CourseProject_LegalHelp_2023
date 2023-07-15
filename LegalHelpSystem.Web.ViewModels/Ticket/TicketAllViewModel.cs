namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    public class TicketAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string TicketCategory { get; set; } = null!;

        public string RequestDescription { get; set; } = null!;

        public bool ResolvedTicketStatus { get; set; }

        public Guid? LegalAdviseId { get; set; }
        public string? Response { get; set; }

        public Guid? DocumentId { get; set; }
        public string? Document { get; set; }


    }
}
