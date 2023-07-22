namespace LegalHelpSystem.Web.ViewModels.Review
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel()
        {
            this.TextReviews = new List<string>();
        }

        public string? Object { get; set; }

        public string? ObjectId { get; set; }

        public virtual ICollection<string> TextReviews { get; set; }

        public int? TotalStars { get; set; }
    }
}
