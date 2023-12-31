﻿namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.DocumentConstants;

    public class Document
    {
        public Document()
        {
            this.Id = Guid.NewGuid();
            this.Reviews = new List<Review>();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DocumentNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int DocumentTypeId { get; set; }

        [Required]
        public virtual DocumentType DocumentType { get; set; } = null!;

        [MaxLength(DocumentDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public byte[] AttachedFile { get; set; } = null!;

        [Required]
        public Guid UploaderId { get; set; }

        [Required]
        public virtual Uploader Uploader { get; set; } = null!;

        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public virtual Ticket Ticket { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; }

    }
}
