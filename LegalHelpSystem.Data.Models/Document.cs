﻿namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.DocumentConstants;

    public class Document
    {
        public Document()
        {
            this.Id = Guid.NewGuid();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int DocumentTypeId { get; set; }

        [Required]
        public virtual DocumentType DocumentType { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public string FileUrl { get; set; } = null!;

        [Required]
        public Guid UploaderId { get; set; }

        [Required]
        public virtual Uploader Uploader { get; set; } = null!;

        public Guid? DownloaderId { get; set; }

        public virtual ApplicationUser? Downloader { get; set; }
    }
}