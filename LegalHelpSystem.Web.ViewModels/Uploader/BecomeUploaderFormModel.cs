namespace LegalHelpSystem.Web.ViewModels.Uploader
{
    using System.ComponentModel.DataAnnotations;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.UploaderConstants;
    public class BecomeUploaderFormModel
    {
        [Required]
        [StringLength(UploaderAuthorRightsDescriptionMaxLength, MinimumLength = UploaderAuthorRightsDescriptionMinLength)]
        [Display(Name = "Description of authors rights")]
        public string AuthorRightsDescription { get; set; } = null!;

    }
}
