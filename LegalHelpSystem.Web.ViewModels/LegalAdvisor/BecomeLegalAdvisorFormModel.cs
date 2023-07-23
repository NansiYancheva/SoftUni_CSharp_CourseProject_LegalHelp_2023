namespace LegalHelpSystem.Web.ViewModels.LegalAdvisor
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeLegalAdvisorFormModel
    {

       [Required]
       [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
