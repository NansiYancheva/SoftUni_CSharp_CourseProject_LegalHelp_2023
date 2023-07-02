namespace LegalHelpSystem.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;


    public class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasData(this.GenerateDocumentTypes());
        }

        private DocumentType[] GenerateDocumentTypes()
        {
            ICollection<DocumentType> documentTypes = new HashSet<DocumentType>();

            DocumentType documentType;
            documentType = new DocumentType()
            {
                Id = 1,
                Name = "Declaration from employee"
            };
            documentTypes.Add(documentType);

            documentType = new DocumentType()
            {
                Id = 2,
                Name = "Employer's Policy"
            };
            documentTypes.Add(documentType);

            documentType = new DocumentType()
            {
                Id = 3,
                Name = "Labour Contract"
            };
            documentTypes.Add(documentType);

            documentType = new DocumentType()
            {
                Id = 4,
                Name = "Additional Agreement"
            };
            documentTypes.Add(documentType);

            documentType = new DocumentType()
            {
                Id = 5,
                Name = "Request from employee"
            };
            documentTypes.Add(documentType);

            documentType = new DocumentType()
            {
                Id = 6,
                Name = "Termination order"
            };
            documentTypes.Add(documentType);

            return documentTypes.ToArray();
        }
    }
}
