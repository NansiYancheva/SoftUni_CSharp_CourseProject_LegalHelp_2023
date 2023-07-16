namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Web.ViewModels.TicketCategory;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.TicketConstants;
    public class TicketAddOrEditFormModel
    {
        public TicketAddOrEditFormModel()
        {
            this.TicketCategories = new HashSet<TicketSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(TicketSubjectMaxLength, MinimumLength = TicketSubjectMinLength)]
        public string Subject { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int TicketCategoryId { get; set; }
        [Required]
        public IEnumerable<TicketSelectCategoryFormModel> TicketCategories { get; set; } 

        [Required]
        [StringLength(TicketDescriptionMaxLength, MinimumLength = TicketDescriptionMinLength)]
        public string RequestDescription { get; set; } = null!;
    }
}
