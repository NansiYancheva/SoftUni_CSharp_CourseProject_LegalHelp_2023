namespace LegalHelpSystem.Web.ViewModels.Document
{
    using LegalHelpSystem.Data.Models;
    public class DocumentAllViewModel
    {
        public DocumentAllViewModel()
        {
            this.Downloaders = new HashSet<ApplicationUser>();
        }
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string DocumentType { get; set; } = null!;

        public string? Description { get; set; }

        public string FileUrl { get; set; } = null!;

        public Guid UploaderId { get; set; }
        public virtual ICollection<ApplicationUser> Downloaders { get; set; }
    }
}
