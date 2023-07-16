namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.UploaderConstants;

    public class Uploader
    {
        public Uploader()
        {
            this.Id = Guid.NewGuid();
            this.UploadedDocuments = new HashSet<Document>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(UploaderAuthorRightsDescriptionMaxLength)]
        public string AuthorRightsDescription { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Document> UploadedDocuments { get; set; }

    }
}
