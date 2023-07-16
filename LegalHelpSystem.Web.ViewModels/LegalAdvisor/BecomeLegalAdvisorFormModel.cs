namespace LegalHelpSystem.Web.ViewModels.LegalAdvisor
{
    using System.ComponentModel.DataAnnotations;

    using static LegalHelpSystem.Common.EntitiesValidationConstants.LegalAdvisorConstants;

    public class BecomeLegalAdvisorFormModel
    {
        [Required]
        [StringLength(LegalAdvisorNameMaxLength, MinimumLength = LegalAdvisorNameMinLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(LegalAdvisorEmailMaxLength, MinimumLength = LegalAdvisorEmailMinLength)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(LegalAdvisorPhoneNumberMaxLength, MinimumLength = LegalAdvisorPhoneNumberMinLength)]
        [RegularExpression(LegalAdvisorPhoneNumberRegulation)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(LegalAdvisorAddressMaxLength, MinimumLength = LegalAdvisorAddressMinLength)]
        public string Address { get; set; } = null!;
    }
}
