namespace LegalHelpSystem.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Data.Models;

    using static Common.EntitiesValidationConstants.ReviewConstants;
    public class ReviewFormModel
    {

        public string? Object { get; set; } 


        public string? ObjectId { get; set; } 

        [Required]
        [StringLength(TextReviewMaxLength, MinimumLength = TextReviewMinLength)]
        public string TextReview { get; set; } = null!;

        [Required]
        [Range(ReviewStarsMin, ReviewStarsMax)]
        public int Stars { get; set; }

        ////////////////////////////////////////////////////////////////
        //public Guid? UploaderId { get; set; }
        //public virtual Uploader? Uploader { get; set; }

        //public Guid? DocumentId { get; set; }
        //public virtual Document? Document { get; set; }

        //public Guid? LegalAdvisorId { get; set; }
        //public virtual LegalAdvisor? LegalAdvisor { get; set; }

        //public Guid? LegalAdviseId { get; set; }
        //public virtual LegalAdvise? LegalAdvise { get; set; }


    }
}
