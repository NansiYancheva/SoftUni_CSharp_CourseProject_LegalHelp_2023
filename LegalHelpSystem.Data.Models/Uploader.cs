namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Uploader
    {
        public Uploader()
        {
            this.Id = Guid.NewGuid();
            this.UploadedDocuments = new HashSet<Document>();
            this.Reviews = new List<Review>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Document> UploadedDocuments { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

    }
}
