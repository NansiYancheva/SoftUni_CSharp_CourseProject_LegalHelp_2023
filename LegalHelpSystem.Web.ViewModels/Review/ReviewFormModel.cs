namespace LegalHelpSystem.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;


    using static Common.EntitiesValidationConstants.ReviewConstants;
    public class ReviewFormModel
    {

        [Required] 
        public string Object { get; set; } = null!;

        [Required] 
        public string ObjectId { get; set; } = null!;  

        [Required]
        [StringLength(TextReviewMaxLength, MinimumLength = TextReviewMinLength)]
        public string TextReview { get; set; } = null!;

        [Required]
        [Range(ReviewStarsMin, ReviewStarsMax)]
        public int Stars { get; set; }

    }
}
