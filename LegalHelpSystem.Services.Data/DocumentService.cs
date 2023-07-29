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
    using LegalHelpSystem.Web.ViewModels.Review;

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
            List<DocumentAllViewModel> listOfAllDocs = await this.dbContext
                .Documents
                .Include(h => h.DocumentType)
                .Include(h => h.Uploader)
                .ThenInclude(h => h.User)
                .Where(x => x.Ticket.RequestDescription != null)
                .Select(h => new DocumentAllViewModel
                {
                    Id = h.Id.ToString(),
                    Name = h.Name,
                    DocumentType = h.DocumentType.Name,
                    Description = h.Description,
                    TicketId = h.TicketId.ToString(),
                    UploaderName = $"{h.Uploader.User.FirstName} {h.Uploader.User.LastName}"
                })
                .ToListAsync();

            return listOfAllDocs;
        }
        //Mine - just listing
        public async Task<IEnumerable<DocumentAllViewModel>> GetMyUploadedDocumentsAsync(string uploaderId)
        {
            List<DocumentAllViewModel> listOfUploadedDocuments =
                 await dbContext.Documents
                .Where(la => la.UploaderId.ToString() == uploaderId)
                .Include(la => la.Uploader)
                .ThenInclude(la => la.User)
                .Select(la => new DocumentAllViewModel
                {
                    Id = la.Id.ToString(),
                    Name = la.Name,
                    DocumentType = la.DocumentType.Name,
                    Description = la.Description,
                    UploaderName = $"{la.Uploader.User.FirstName} {la.Uploader.User.LastName}"
                })
                .ToListAsync();

            return listOfUploadedDocuments;
        }

        //Downloaded
        public async Task<IEnumerable<DocumentAllViewModel>> GetDownloadedByUserAsync(string userId)
        {
            ApplicationUser user = await this.dbContext
                .Users
                .Include(x => x.DownloadedByUserDocs)
                .ThenInclude(x => x.DocumentType)
                .Include(x=> x.DownloadedByUserDocs)
                .ThenInclude(x => x.Reviews)
                .Include(x => x.DownloadedByUserDocs)
                .ThenInclude(x => x.Uploader)
                .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id.ToString() == userId);
            List<Document> userDocs = user.DownloadedByUserDocs.ToList();

            List<DocumentAllViewModel> allDownloadedByUser = new List<DocumentAllViewModel>();
                
            foreach (Document doc in userDocs)
            {
                DocumentAllViewModel currDoc = new DocumentAllViewModel
                {
                    Id = doc.Id.ToString(),
                    Name = doc.Name,
                    DocumentType = doc.DocumentType.Name,
                    Description = doc.Description,
                    DocumentFile = doc.AttachedFile,
                    UploaderId = doc.UploaderId,
                    TicketId = doc.TicketId.ToString(),
                    Reviews = await GetDocumentReviews(doc.Id.ToString()),
                    UploaderName = $"{doc.Uploader.User.FirstName} {doc.Uploader.User.LastName}"
                };
                allDownloadedByUser.Add(currDoc);
            }


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

        //Common
        public async Task<bool> DocumentExistsByIdAsync(string objectId)
        {
            bool result = await this.dbContext
                .Documents
                .AnyAsync(a => a.Id.ToString() == objectId);

            return result;
        }

        public async Task<string> GetDocumentNameAsync(string objectId)
        {
            Document document = await this.dbContext
                .Documents
                .FirstOrDefaultAsync(x => x.Id.ToString() == objectId);

            return document.Name;
        }

        public async Task<ReviewsViewModel> GetDocumentReviews(string id)
        {
            Document document = await this.dbContext
           .Documents
           .Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            List<string> listOfTextReviews = document.Reviews
                .Select(x => x.TextReview)
                .ToList();

            int totalStars = document.Reviews
                .Select(x => x.Stars)
                .Sum();

            int aggTotalStars;
            if (totalStars == 0)
            {
                aggTotalStars = 0;
            }
            else
            {
                aggTotalStars = totalStars / document.Reviews.Count;
            }

            return new ReviewsViewModel
            {
                Object = document.Name,
                TextReviews = listOfTextReviews,
                TotalStars = aggTotalStars
            };
        }
    }
}
