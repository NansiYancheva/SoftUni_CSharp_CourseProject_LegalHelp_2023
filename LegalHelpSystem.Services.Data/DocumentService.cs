namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Document;
    using LegalHelpSystem.Data.Models;


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
                    TicketId = h.TicketId.ToString()
                   // FileUrl = h.Attachment
                    //UploaderId = h.UploaderId,
                    //Downloaders = h.Downloaders
                    //ToDo: to add how many times the document was downloaded
                })
                .ToListAsync();
        }
        //Mine - just listing
        public async Task<IEnumerable<DocumentAllViewModel>> GetMyUploadedDocumentsAsync(string uploaderId)
        {
            List<DocumentAllViewModel> listOfUploadedDocuments =
                 await dbContext.Documents
                .Where(la => la.UploaderId.ToString() == uploaderId)
                .Select(la => new DocumentAllViewModel
                {
                    //Id = la.Id.ToString(),
                    Name = la.Name,
                    DocumentType = la.DocumentType.Name,
                    Description = la.Description,
                    //FileUrl = la.Attachment
                    //UploaderId = la.UploaderId,
                    //Downloaders = la.Downloaders
                })
                .ToListAsync();

            return listOfUploadedDocuments;
        }

        //Downloaded
        public async Task<IEnumerable<DocumentAllViewModel>> GetDownloadedByUserAsync(string userId)
        {
            Guid userIdToGuid = Guid.Parse(userId);
            List<Document> listOfDocuments =
                 await dbContext.Documents
                 .Join(dbContext.Users.Where(u => u.Id == userIdToGuid),
                  document => document.Id,
                  user => user.Id,
                  (document, user) => document)
                 .ToListAsync();
            List<DocumentAllViewModel> listOfDownloadedDocuments =
            listOfDocuments
                 .Select(la => new DocumentAllViewModel
                 {
                     //Id = la.Id.ToString(),
                     Name = la.Name,
                     DocumentType = la.DocumentType.Name,
                     Description = la.Description,
                    // FileUrl = la.Attachment
                     //UploaderId = la.UploaderId,
                     //Downloaders = la.Downloaders
                 })
                 .ToList();

            return listOfDownloadedDocuments;

        }

        //Add Document-Post
        public async Task<string> UploadDocumentAsync(DocumentFormModel formModel, string uploaderId, byte[] fileBytes)
        {
            Document document = new Document
            {
                Name = formModel.DocumentName,
                DocumentTypeId = formModel.DocumentTypeId,
                Description = formModel.DocumentDescription,
                AttachedFile = fileBytes,
                UploaderId = Guid.Parse(uploaderId),
                TicketId = Guid.Parse(formModel.TicketId)
            };

            await this.dbContext.Documents.AddAsync(document);
            await this.dbContext.SaveChangesAsync();

            return document.Id.ToString();
        }

        //Download document
        public async Task<DocumentForDownloadViewModel> GetDocumentForDownload(string ticketId)
        {
            Document document = await this.dbContext
               .Documents
               .FirstAsync(x => x.TicketId.ToString() == ticketId);

            return new DocumentForDownloadViewModel
            {
               DocumentName = document.Name,
               DocumentFile = document.AttachedFile
            };
        }
    }
}
