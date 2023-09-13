namespace LegalHelpSystem.Web.ViewModels.Document
{
    using System.ComponentModel.DataAnnotations;

    using LegalHelpSystem.Web.ViewModels.DocumentType;


    public class DocumentFormForFillInModel
    {
        public DocumentFormForFillInModel()
        {
            this.Types = new HashSet<DocumentSelectTypeFormModel>();
        }

        public string? Id { get; set; }

        [Required]
        [Display(Name = "Fill In Your Name")]
        public string UserName { get; set; } = null!;

        public int DocumentTypeId { get; set; }


        public IEnumerable<DocumentSelectTypeFormModel> Types { get; set; }
    }
}
