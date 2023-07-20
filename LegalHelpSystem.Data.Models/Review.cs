namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.ReviewConstants;
    public class Review
    {
        public Review()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(TextReviewMaxLength)]
        public string TextReview { get; set; } = null!;

        [Required]
        public int Stars { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Guid? UploaderId { get; set; }
        public virtual Uploader? Uploader { get; set; }

        public Guid? DocumentId { get; set; }
        public virtual Document? Document { get; set; }

        public Guid? LegalAdvisorId { get; set; }
        public virtual LegalAdvisor? LegalAdvisor { get; set; }

        public Guid? LegalAdviseId { get; set; }
        public virtual LegalAdvise? LegalAdvise { get; set; }



    }
}
