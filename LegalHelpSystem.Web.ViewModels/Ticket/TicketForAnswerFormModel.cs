namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Data.Models;
    using static LegalHelpSystem.Common.EntitiesValidationConstants.TicketConstants;

    public class TicketForAnswerFormModel
    {

        [Required]
        [StringLength(SubjectMaxLength, MinimumLength = SubjectMinLength)]
        public string Subject { get; set; } = null!;


        [Required]
        [StringLength(RequestDescriptionMaxLength, MinimumLength = RequestDescriptionMinLength)]
        public string RequestDescription { get; set; } = null!;

        public virtual LegalAdvise? Response { get; set; }
    }
}
