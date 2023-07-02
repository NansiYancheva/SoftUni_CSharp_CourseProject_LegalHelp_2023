namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.LegalAdviseConstants;

    public class LegalAdvise
    {
        public LegalAdvise()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(AdviseResponseMaxLength)]
        public string AdviseResponse { get; set; } = null!;

        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public virtual Ticket Ticket { get; set; } = null!;

        [Required]
        public Guid LegalAdvisorId { get; set; }
        [Required]
        public virtual LegalAdvisor LegalAdvisor { get; set; } = null!;
    }
}
