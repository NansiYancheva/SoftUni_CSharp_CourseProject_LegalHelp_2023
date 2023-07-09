namespace LegalHelpSystem.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;

    public class LegalAdvisorEntityConfiguration : IEntityTypeConfiguration<LegalAdvisor>
    {
        public void Configure(EntityTypeBuilder<LegalAdvisor> builder)
        {
            builder
                .Property(r => r.Rating)
                .HasPrecision(18, 2); 
        }
    }
}
