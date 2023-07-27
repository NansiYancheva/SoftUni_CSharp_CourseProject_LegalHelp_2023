namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntitiesValidationConstants.LegalAdviseConstants;
    public class LegalAdviseFormModel
    {
        [Display(Name = "Legal Advise")]

        [Required]
        [StringLength(AdviseResponseMaxLength, MinimumLength = AdviseResponseMinLength)]
        public string AdviseResponse { get; set; }

        [Required]
        public string TicketId { get; set; }

        [Required]
        public  string TicketSubject { get; set; }

        [Required]
        public  string TicketDescription { get; set; } 
    }
}
