namespace LegalHelpSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.DownloadedDocuments = new HashSet<Document>();
            this.Requests = new HashSet<Ticket>();
        }
        public virtual ICollection<Document> DownloadedDocuments { get; set; }
        public virtual ICollection<Ticket> Requests { get; set; }
    }
}
