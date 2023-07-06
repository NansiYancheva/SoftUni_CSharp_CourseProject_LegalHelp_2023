namespace LegalHelpSystem.Web.ViewModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.ViewModels.TicketCategory;
    using static LegalHelpSystem.Common.EntitiesValidationConstants.TicketConstants;

    public class TicketForAnswerFormModel
    {
        public TicketForAnswerFormModel()
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

        //null or not null?

        public Guid? LegalAdviseId { get; set; }
        //should the naming be changed?
        public virtual LegalAdvise? Response { get; set; }
    }
}
