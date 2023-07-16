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

        [Required]
        public string TicketId { get; set; }

        [Required]
        [StringLength(TicketSubjectMaxLength, MinimumLength = TicketSubjectMinLength)]
        public string TicketSubject { get; set; }

        [Required]
        [StringLength(TicketDescriptionMaxLength, MinimumLength = TicketDescriptionMinLength)]
        public string TicketDescription { get; set; }

        [Required]
        [StringLength(DocumentNameMaxLength, MinimumLength = DocumentNameMinLength)]
        public string DocumentName { get; set; } = null!;

        [StringLength(DocumentDescriptionMaxLength)]
        public string? DocumentDescription { get; set; }

        [Display(Name = "Document Link")]
        public string DocumentForUploadFileUrl { get; set; }

        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }
        public IEnumerable<DocumentSelectTypeFormModel> Types { get; set; }
    }
}
