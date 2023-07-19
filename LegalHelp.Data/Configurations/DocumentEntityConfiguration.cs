namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;


    public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder
                .HasOne(d => d.DocumentType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(d => d.Uploader)
                .WithMany(u => u.UploadedDocuments)
                .HasForeignKey(d => d.UploaderId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        //private Document[] GenerateDocuments()
        //{
        //    ICollection<Document> documents = new HashSet<Document>();

        //    Document document;

        //    document = new Document()
        //    {
        //        Name = "Declaration bank account",
        //        DocumentTypeId = 1,
        //        Description = "Declaration consent from employee under art. 270 from the Labour Code",
        //        Attachment = ,
        //        UploaderId = Guid.Parse("9468D60F - 0002 - 48EE - A1DD - 084FC5D1C51F"),
        //        DownloaderId = Guid.Parse("9783EA53-DA82-46BD-EDCF-08DB78029CE0")
        //    };

        //    documents.Add(document);
        //}
    }
}
