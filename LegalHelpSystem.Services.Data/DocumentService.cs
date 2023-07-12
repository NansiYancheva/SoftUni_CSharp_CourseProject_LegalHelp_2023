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
                   // Id = h.Id.ToString(),
                    Name = h.Name,
                    DocumentType = h.DocumentType.Name,
                    Description = h.Description,
                    FileUrl = h.FileUrl
                    //UploaderId = h.UploaderId,
                    //Downloaders = h.Downloaders
                    //ToDo: to add how many times the document was downloaded
                })
                .ToListAsync();
        }
        //Mine - just listing
        public async Task<IEnumerable<DocumentAllViewModel>> GetMyUploadedDocumentsAsync(string uploaderId)
        {
            var listOfUploadedDocuments =
                 await dbContext.Documents
                .Where(la => la.UploaderId.ToString() == uploaderId)
                .Select(la => new DocumentAllViewModel
                {
                    //Id = la.Id.ToString(),
                    Name = la.Name,
                    DocumentType = la.DocumentType.Name,
                    Description = la.Description,
                    FileUrl = la.FileUrl
                    //UploaderId = la.UploaderId,
                    //Downloaders = la.Downloaders
                })
                .ToListAsync();

            return listOfUploadedDocuments;
        }
    }
}
