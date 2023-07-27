namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using LegalHelpSystem.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class TicketAllViewModel
    {
        public string Id { get; set; } = null!;
        public string Subject { get; set; } = null!;

        [Display(Name = "Category")]
        public string TicketCategory { get; set; } = null!;

        [Display(Name = "Request")]
        public string RequestDescription { get; set; } = null!;

        [Display(Name = "Status")]
        public bool ResolvedTicketStatus { get; set; }

        public Guid? LegalAdviseId { get; set; }
        public string? Response { get; set; }
        public string LegalAdvisorName { get; set; }

        public string? LegalAdvisorUserId { get; set; }
        public string? LegalAdvisorId { get; set; }

        public Guid? DocumentId { get; set; }
        public string? Document { get; set; }
        public Uploader? Uploader { get; set; }
        public string? UploaderUserId { get; set; }
        public string? UploaderId { get; set; }


    }
}
