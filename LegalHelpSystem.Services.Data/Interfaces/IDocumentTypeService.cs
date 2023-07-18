namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.DocumentType;

    public interface IDocumentTypeService
    {
        Task<IEnumerable<DocumentSelectTypeFormModel>> AllDocumentTypesAsync();
        Task<bool> ExistsByIdAsync(int documentTypeId);
        
    }
}
