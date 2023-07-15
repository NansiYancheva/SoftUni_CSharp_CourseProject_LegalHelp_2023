namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    public class TicketForAnswerFormModel
    {
        [Required]
        public string Subject { get; set; } = null!;
        [Required]
        public string RequestDescription { get; set; } = null!;
    }
}
