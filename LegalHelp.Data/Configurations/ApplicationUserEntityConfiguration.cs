namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data.Models;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
    //        builder
    //.Property(x => x.DownloadedDocs)
    //.HasColumnType("nvarchar(MAX)");

            builder
            .HasMany(d => d.DownloadedDocs)
            .WithMany(d => d.Downloaders);


        }
    }
}
