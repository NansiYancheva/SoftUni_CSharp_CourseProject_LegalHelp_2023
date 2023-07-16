namespace LegalHelpSystem.Web.ViewModels.Document
{
    public class DocumentForDownloadViewModel
    {

        public string DocumentName { get; set; } = null!;

        public byte[] DocumentFile { get; set; } = null!;
    }
}
