namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.DocumentTypeConstants;

    public class DocumentType
    {
        public DocumentType()
        {
            this.Documents = new HashSet<Document>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; }
    }
}
