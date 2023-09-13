namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LegalHelpSystem.Web.ViewModels.DocumentType;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;

    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly LegalHelpDbContext dbContext;

        public DocumentTypeService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DocumentSelectTypeFormModel>> AllDocumentTypesAsync()
        {
            IEnumerable<DocumentSelectTypeFormModel> allTypes = await this.dbContext
               .DocumentTypes
               .AsNoTracking()
               .Select(c => new DocumentSelectTypeFormModel()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .ToArrayAsync();

            return allTypes;
        }

        public async Task<bool> ExistsByIdAsync(int documentTypeId)
        {
            bool result = await this.dbContext
               .DocumentTypes
               .AnyAsync(t => t.Id == documentTypeId);

            return result;
        }
    }
}
