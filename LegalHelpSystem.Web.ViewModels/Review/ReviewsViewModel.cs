using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LegalHelpSystem.Web.ViewModels.Review
{
    public class ReviewsViewModel
    {
        public ReviewsViewModel()
        {
            this.TextReviews = new List<string>();
        }

        public string? Object { get; set; }

        [Display(Name = "Text Reviews")]
        public virtual ICollection<string> TextReviews { get; set; }

        public int? TotalStars { get; set; }
    }
}
