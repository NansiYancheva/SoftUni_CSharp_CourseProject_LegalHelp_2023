namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using LegalHelpSystem.Data.Models;
    public class TicketAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string TicketCategory { get; set; } = null!;

        public string RequestDescription { get; set; } = null!;

        public bool ResolvedTicketStatus { get; set; }

        public Guid? LegalAdviseId { get; set; }
        public string? Response { get; set; }

        public LegalAdvisor? LegalAdvisor { get; set; }
        public string? LegalAdvisorUserId { get; set; }

        public Guid? DocumentId { get; set; }
        public string? Document { get; set; }

        public Uploader? Uploader { get; set; }
        public string? UploaderUserId { get; set; }


    }
}
