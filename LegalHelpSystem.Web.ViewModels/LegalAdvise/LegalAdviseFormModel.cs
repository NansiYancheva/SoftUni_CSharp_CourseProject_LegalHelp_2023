using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    public class LegalAdviseFormModel
    {
        [Display(Name = "Legal Advise")]
        public string AdviseResponse { get; set; } 
        public string TicketId { get; set; } 
        public  string TicketSubject { get; set; } 
        public  string TicketDescription { get; set; } 
    }
}
