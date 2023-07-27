using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    public class TicketByNoUserViewModel
    {
        public string Subject { get; set; } = null!;

        [Display(Name = "Category")]
        public string TicketCategory { get; set; } = null!;

        [Display(Name = "Request")]
        public string RequestDescription { get; set; } = null!;

        [Display(Name = "Status")]
        public bool ResolvedTicketStatus { get; set; }
    }
}
