namespace LegalHelpSystem.Web.ViewModels.Uploader
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeUploaderFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

    }
}
