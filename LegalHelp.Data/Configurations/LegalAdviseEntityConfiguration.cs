namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;

    public class LegalAdviseEntityConfiguration : IEntityTypeConfiguration<LegalAdvise>
    {

        public void Configure(EntityTypeBuilder<LegalAdvise> builder)
        {

            builder
                .HasOne(la => la.LegalAdvisor)
                .WithMany(la => la.LegalAdvises)
                .HasForeignKey(la => la.LegalAdvisorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.Ticket)
                .WithOne(t => t.Response)
                .HasForeignKey<LegalAdvise>(t => t.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //.Property(a => a.TicketId)
            //.IsRequired(false);
        }
    }
}
