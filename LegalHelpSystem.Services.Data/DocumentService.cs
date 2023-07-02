namespace LegalHelpSystem.Services.Data
{
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;

    public class DocumentService : IDocumentService
    {
        private readonly LegalHelpDbContext dbContext;
        public DocumentService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
