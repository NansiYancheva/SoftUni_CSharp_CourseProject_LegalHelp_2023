namespace LegalHelpSystem.Web.ViewModels.User
{
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.ViewModels.Review;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class AllTeamMembersViewModel
    {
        [Display(Name = "Legal Advisor Name")]
        public string? LegalAdvisorName { get; set; }
        public string? LegalAdvisorUserId { get; set; }

        [Display(Name = "Legal Advisor Reviews")]
        public virtual ReviewsViewModel LegalAdvisorReviews { get; set; }

        [Display(Name = "Uploader Name")]
        public Uploader? Uploader { get; set; }
        public string? UploaderUserId { get; set; }

        [Display(Name = "Uploader Reviews")]
        public virtual ReviewsViewModel UploaderReviews { get; set; }
    }
}
