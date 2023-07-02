namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using LegalHelpSystem.Web.ViewModels.TicketCategory;
    using System.ComponentModel.DataAnnotations;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.TicketConstants;
    public class TicketAddOrEditFormModel
    {
        public TicketAddOrEditFormModel()
        {
            this.TicketCategories = new HashSet<TicketSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(SubjectMaxLength, MinimumLength = SubjectMinLength)]
        public string Subject { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int TicketCategoryId { get; set; }
        [Required]
        public IEnumerable<TicketSelectCategoryFormModel> TicketCategories { get; set; } 

        [Required]
        [StringLength(RequestDescriptionMaxLength, MinimumLength = RequestDescriptionMinLength)]
        public string RequestDescription { get; set; } = null!;
    }
}
