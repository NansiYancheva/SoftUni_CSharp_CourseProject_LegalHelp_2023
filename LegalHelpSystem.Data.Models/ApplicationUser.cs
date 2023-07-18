namespace LegalHelpSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.DownloadedDocuments = new List<Document>();
        }
        public virtual ICollection<Document> DownloadedDocuments { get; set; }
    }
}
