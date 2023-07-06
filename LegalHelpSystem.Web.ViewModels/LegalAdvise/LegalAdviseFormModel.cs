namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    using System.ComponentModel.DataAnnotations;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.LegalAdviseConstants;

    public class LegalAdviseFormModel
    {
        [Required]
        [StringLength(AdviseResponseMaxLength, MinimumLength = AdviseResponseMinLength)]
        public string AdviseResponse { get; set; } = null!;
    }
}
