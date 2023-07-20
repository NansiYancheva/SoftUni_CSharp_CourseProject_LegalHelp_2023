namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;


    public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
            .Property(x => x.CreatedOn)
            .HasDefaultValueSql("GETDATE()");

            builder
            .HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.Document)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.Uploader)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UploaderId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.LegalAdvise)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.LegalAdviseId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(x => x.LegalAdvisor)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.LegalAdvisorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
