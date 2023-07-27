namespace LegalHelpSystem.Web.ViewModels.Document
{
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.ViewModels.Review;

    public class DocumentAllViewModel
    {
        public DocumentAllViewModel()
        {

        }

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string DocumentType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] DocumentFile { get; set; } 

        public Guid UploaderId { get; set; }
        public string UploaderName { get; set; }
        public string UploaderUserId { get; set; }

        public ReviewsViewModel Reviews { get; set; }

        public string TicketId { get; set; } = null!;
    }
}
