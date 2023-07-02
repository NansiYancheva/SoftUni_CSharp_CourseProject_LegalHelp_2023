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
        [MaxLength(NameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        public decimal? Rating { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<LegalAdvise> LegalAdvises { get; set; }
    }
}
