namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    //using System.ComponentModel.DataAnnotations;
    using LegalHelpSystem.Data.Models;

    //using static LegalHelpSystem.Common.EntitiesValidationConstants.LegalAdviseConstants;

    public class LegalAdviseFormModel
    {
        //[Required]
        //[StringLength(AdviseResponseMaxLength, MinimumLength = AdviseResponseMinLength)]
        public string AdviseResponse { get; set; } //= null!;
        public string TicketId { get; set; } 
        public  string TicketSubject { get; set; } 

        public  string TicketDescription { get; set; } 
        //public static Ticket Ticket { get; set; } 
        //public string LegalAdvisorId { get; set; } = null!;
        //[Required]
        //public virtual LegalAdvisor LegalAdvisor { get; set; } = null!;
    }
}
