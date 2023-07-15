using LegalHelpSystem.Web.ViewModels.DocumentType;

namespace LegalHelpSystem.Services.Data.Interfaces
{
    public interface IDocumentTypeService
    {
        Task<IEnumerable<DocumentSelectTypeFormModel>> AllDocumentTypesAsync();
        Task<bool> ExistsByIdAsync(int documentTypeId);
        
    }
}
