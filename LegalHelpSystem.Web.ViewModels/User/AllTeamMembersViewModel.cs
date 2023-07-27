namespace LegalHelpSystem.Web.ViewModels.User
{
    using LegalHelpSystem.Web.ViewModels.Review;
    using System.ComponentModel.DataAnnotations;


    public class AllTeamMembersViewModel
    {
        [Display(Name = "Legal Advisor Name")]
        public string? LegalAdvisorName { get; set; }
        public string? LegalAdvisorUserId { get; set; }

        [Display(Name = "Legal Advisor Reviews")]
        public ReviewsViewModel LegalAdvisorReviews { get; set; }

        [Display(Name = "Uploader Name")]
        public string? UploaderName { get; set; }
        public string? UploaderUserId { get; set; }

        [Display(Name = "Uploader Reviews")]
        public ReviewsViewModel UploaderReviews { get; set; }
    }
}
