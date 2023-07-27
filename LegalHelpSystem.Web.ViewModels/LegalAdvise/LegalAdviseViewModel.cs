using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    public class LegalAdviseViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "Legal Advise")]
        public string AdviseResponse { get; set; } = null!;

        [Display(Name = "Ticket Subject")]
        public string TicketSubject { get; set; } = null!;

        [Display(Name = "Ticket Description")]
        public string TicketDescription { get; set; } = null!;

        [Display(Name = "Legal Advisor Name")]
        public string LegalAdvisorName { get; set; } = null!;

        public string LegalAdvisorUserId { get; set; } = null!;

    }
}
