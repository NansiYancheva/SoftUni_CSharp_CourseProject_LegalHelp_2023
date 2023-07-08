namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Data.Models;
    using static LegalHelpSystem.Common.EntitiesValidationConstants.TicketConstants;

    public class TicketForAnswerFormModel
    {

        [Required]
        public string Subject { get; set; } = null!;


        [Required]
        public string RequestDescription { get; set; } = null!;
    }
}
