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
                .Where(x => x.Ticket.RequestDescription != null)
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


            //ApplicationUser user = await this.dbContext
            //    .ApplicationUsers
            //    .FirstAsync(x => x.Id.ToString() == userId);



            // List<Document> userDocs = user.DownloadedDocs.ToList();

            List<DocumentAllViewModel> allDownloadedByUser = new List<DocumentAllViewModel>(); 
            //userDocs
            //    .Select(x => new DocumentAllViewModel
            //    {
            //        Name = x.Name,
            //        DocumentType = x.DocumentType.Name,
            //        Description = x.Description,
            //        DocumentFile = x.AttachedFile,
            //        TicketId = x.TicketId.ToString()
            //    })
            //    .ToList();

            return allDownloadedByUser;

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

        public async Task AddUserToDocDownloadersCollectionAsync(string userId, string ticketId)
        {
            Document foundDocument = await this.dbContext
                 .Documents
                 .FirstAsync(x => x.TicketId.ToString() == ticketId);

            //ApplicationUser user = await this.dbContext
            //     .ApplicationUsers
            //     .FirstAsync(x => x.Id.ToString() == userId);

           // foundDocument.Downloaders.Add(user);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
