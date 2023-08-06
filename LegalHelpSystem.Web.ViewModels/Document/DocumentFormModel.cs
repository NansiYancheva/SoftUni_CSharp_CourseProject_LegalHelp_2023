namespace LegalHelpSystem.Web.ViewModels.Document
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Web.ViewModels.DocumentType;
    using static Common.EntitiesValidationConstants.DocumentConstants;
    using static Common.EntitiesValidationConstants.TicketConstants;

    public class DocumentFormModel
    {
        public DocumentFormModel()
        {
            this.Types = new HashSet<DocumentSelectTypeFormModel>();
        }
        public string? Id { get; set; }

        [Required]
        public string TicketId { get; set; } = null!;

        [Required]
        [StringLength(TicketSubjectMaxLength, MinimumLength = TicketSubjectMinLength)]
        [Display(Name = "Ticket Subject")]
        public string TicketSubject { get; set; } = null!;

        [Required]
        [StringLength(TicketDescriptionMaxLength, MinimumLength = TicketDescriptionMinLength)]
        [Display(Name = "Ticket Description")]
        public string TicketDescription { get; set; } = null!;

        [Required]
        [StringLength(DocumentNameMaxLength, MinimumLength = DocumentNameMinLength)]
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; } = null!;

        [StringLength(DocumentDescriptionMaxLength)]
        [Display(Name = "Document Description")]
        public string DocumentDescription { get; set; } = null!;

        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }
        public IEnumerable<DocumentSelectTypeFormModel> Types { get; set; }
    }
}
