namespace LegalHelpSystem.Web.ViewModels.Uploader
{
    using System.ComponentModel.DataAnnotations;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.UploaderConstants;
    public class BecomeUploaderFormModel
    {
        [Required]
        [StringLength(AuthorRightsDescriptionMaxLength, MinimumLength = AuthorRightsDescriptionMinLength)]
        [Display(Name = "Description of authors rights")]
        public string AuthorRightsDescription { get; set; } = null!;

    }
}
