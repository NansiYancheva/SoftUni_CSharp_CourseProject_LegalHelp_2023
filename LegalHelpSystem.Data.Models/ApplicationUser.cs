namespace LegalHelpSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.UserConstants;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Reviews = new List<Review>();
            this.DownloadedByUserDocs = new List<Document>();
        }
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        //[Required]
        //[MaxLength(LastNameMaxLength)]
        //public string Email { get; set; } = null!;

        public virtual ICollection<Document> DownloadedByUserDocs { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
