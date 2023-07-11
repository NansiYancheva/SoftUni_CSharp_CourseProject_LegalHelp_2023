namespace LegalHelpSystem.Services.Data
{
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Document;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DocumentService : IDocumentService
    {
        private readonly LegalHelpDbContext dbContext;
        public DocumentService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //All - get
        public Task<IEnumerable<DocumentAllViewModel>> GetAllDocumentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
