namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntitiesValidationConstants.LegalAdviseConstants;
    public class LegalAdviseFormModel
    {


        [Required]
        [StringLength(AdviseResponseMaxLength, MinimumLength = AdviseResponseMinLength)]
        [Display(Name = "Legal Advise")]
        public string AdviseResponse { get; set; } = null!;

        [Required]
        public string TicketId { get; set; } = null!;

        [Required]
        [Display(Name = "Ticket Subject")]
        public  string TicketSubject { get; set; } = null!;

        [Required]
        [Display(Name = "Ticket Description")]
        public  string TicketDescription { get; set; } = null!;
    }
}
