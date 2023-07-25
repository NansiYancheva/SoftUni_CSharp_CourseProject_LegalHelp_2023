namespace LegalHelpSystem.Web.ViewModels.User
{
    using LegalHelpSystem.Data.Models;
    public class AllTeamMembersViewModel
    {
        public AllTeamMembersViewModel()
        {
            this.LegalAdvisorReviews = new List<Review>();
            this.UploaderReviews = new List<Review>();
        }
        public LegalAdvisor? LegalAdvisor { get; set; }
        public string? LegalAdvisorUserId { get; set; }

        public virtual ICollection<Review> LegalAdvisorReviews { get; set; }

        public Uploader? Uploader { get; set; }
        public string? UploaderUserId { get; set; }
        public virtual ICollection<Review> UploaderReviews { get; set; }
    }
}
