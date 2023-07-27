namespace LegalHelpSystem.Web.ViewModels.Document
{
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.ViewModels.Review;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class DocumentAllViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "Document Name")]
        public string Name { get; set; } = null!;
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; } = null!;
        [Display(Name = "Document Description")]
        public string Description { get; set; } = null!;
        [Display(Name = "Document File")]
        public byte[] DocumentFile { get; set; } 

        public Guid UploaderId { get; set; }
        [Display(Name = "Uploader Name")]
        public string UploaderName { get; set; }
        public string UploaderUserId { get; set; }

        [Display(Name = "Document Reviews")]
        public ReviewsViewModel Reviews { get; set; }

        public string TicketId { get; set; } = null!;
    }
}
