namespace LegalHelpSystem.Web.ViewModels.Document
{
    using LegalHelpSystem.Data.Models;
    public class DocumentAllViewModel
    {
        public DocumentAllViewModel()
        {
            this.Downloaders = new HashSet<ApplicationUser>();
            this.Reviews = new List<Review>();
        }
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string DocumentType { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[] DocumentFile { get; set; } 

        public Guid UploaderId { get; set; }
        public Uploader Uploader { get; set; }
        public virtual ICollection<ApplicationUser> Downloaders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public string TicketId { get; set; } = null!;
    }
}
