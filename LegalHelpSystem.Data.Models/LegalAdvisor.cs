namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

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
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<LegalAdvise> LegalAdvises { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
