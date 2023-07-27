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

        public Guid? LegalAdviseId { get; set; }
        public virtual LegalAdvise? Response { get; set; }
    }
}
