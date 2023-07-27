namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using LegalHelpSystem.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class TicketPerDeleteFormModel
    {
        public string Subject { get; set; } = null!;

        [Display(Name = "Request")]
        public string RequestDescription { get; set; } = null!;

        [Display(Name = "Status")]
        public bool ResolvedTicketStatus { get; set; }

        [Display(Name = "Legal Advise")]
        public string? Response { get; set; }


        [Display(Name = "Document Name")]
        public string? DocumentName { get; set; }
    }
}
