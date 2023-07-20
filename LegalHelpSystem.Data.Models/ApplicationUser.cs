namespace LegalHelpSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Reviews = new List<Review>();
        }

        public virtual ICollection<Review> Reviews { get; set; }

    }
}
