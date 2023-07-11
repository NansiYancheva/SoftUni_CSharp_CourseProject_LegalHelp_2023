namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Document;
    using System.Xml.Linq;


    public class DocumentService : IDocumentService
    {
        private readonly LegalHelpDbContext dbContext;
        public DocumentService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //All - get
        public async Task<IEnumerable<DocumentAllViewModel>> GetAllDocumentsAsync()
        {
            return await this.dbContext
                .Documents
                .Include(h => h.DocumentType)
                .Include(h => h.Uploader)
                .Select(h => new DocumentAllViewModel
                {
                    Id = h.Id.ToString(),
                    Name = h.Name,
                    DocumentType = h.DocumentType.Name,
                    Description = h.Description,
                    FileUrl = h.FileUrl,
                    UploaderId = h.UploaderId,
                    Downloaders = h.Downloaders
                })
                .ToListAsync();
        }
    }
}
