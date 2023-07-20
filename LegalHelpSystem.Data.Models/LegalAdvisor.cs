namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.LegalAdvisorConstants;

    public class LegalAdvisor
    {
        public LegalAdvisor()
        {
            this.Id = Guid.NewGuid();
            this.LegalAdvises = new HashSet<LegalAdvise>();
            this.Reviews = new List<Review>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(LegalAdvisorNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(LegalAdvisorEmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(LegalAdvisorPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(LegalAdvisorAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<LegalAdvise> LegalAdvises { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
