using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LegalHelpSystem.Web.ViewModels.Document
{
    public class DocumentForDownloadViewModel
    {
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; } = null!;
        [Display(Name = "Document File")]
        public byte[] DocumentFile { get; set; } = null!;
    }
}
