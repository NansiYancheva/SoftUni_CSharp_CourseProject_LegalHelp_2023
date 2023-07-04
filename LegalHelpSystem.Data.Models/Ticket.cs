﻿namespace LegalHelpSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntitiesValidationConstants.TicketConstants;
    public class Ticket
    {
        public Ticket()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(SubjectMaxLength)]
        public string Subject { get; set; } = null!;

        [Required]
        public int TicketCategoryId { get; set; }
        [Required]
        public virtual TicketCategory TicketCategory { get; set; } = null!;

        [Required]
        [MaxLength(RequestDescriptionMaxLength)]
        public string RequestDescription { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public int TicketStatusId { get; set; } 
        [Required]
        public virtual TicketStatus TicketStatus { get; set; } = null!;

        public Guid? LegalAdviseId { get; set; }
        //should the naming be changed?
        public virtual LegalAdvise? Response { get; set; }
    }
}
