namespace LegalHelpSystem.Web.ViewModels.Document
{
    using System.ComponentModel.DataAnnotations;
    using LegalHelpSystem.Web.ViewModels.DocumentType;
    public class DocumentFormModel
    {
        public DocumentFormModel()
        {
            this.Types = new HashSet<DocumentSelectTypeFormModel>();
        }
        public string TicketId { get; set; }
        public string TicketSubject { get; set; }
        public string TicketDescription { get; set; }

        public string DocumentForUploadFileUrl { get; set; }
        public string DocumentDescription { get; set; }

        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        public IEnumerable<DocumentSelectTypeFormModel> Types { get; set; }
    }
}
