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

        public decimal? Rating { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<LegalAdvise> LegalAdvises { get; set; }
    }
}
