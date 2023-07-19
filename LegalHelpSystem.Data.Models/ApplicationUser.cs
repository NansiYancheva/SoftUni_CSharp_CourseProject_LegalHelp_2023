namespace LegalHelpSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
          //  this.DownloadedDocuments = new List<Document>();
            this.DownloadedDocs = new List<Document>();
        }
       //public virtual List<Document> DownloadedDocuments { get; set; }

        public virtual List<Document> DownloadedDocs { get; set; }
    }
}
