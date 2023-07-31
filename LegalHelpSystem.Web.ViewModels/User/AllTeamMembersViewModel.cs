namespace LegalHelpSystem.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Web.ViewModels.Review;

    public class AllTeamMembersViewModel
    {
        [Display(Name = "User Name")]
        public string? UserName { get; set; }
        public string? UserId { get; set; }

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

        public string Email { get; set; }
    }
}
