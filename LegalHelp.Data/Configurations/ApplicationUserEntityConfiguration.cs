namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
            .HasMany(d => d.DownloadedDocuments)
            .WithMany(d => d.Downloaders);
        }
    }
}
