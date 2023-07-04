namespace LegalHelpSystem.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;

    public class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasOne(s => s.TicketStatus)
                .WithMany(t => t.Tickets)
                .HasForeignKey(s => s.TicketStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.TicketCategory)
                .WithMany(t => t.Tickets)
                .HasForeignKey(c => c.TicketCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //the naming can be a problem? Response and LegalAdvise
            builder
                .HasOne(t => t.Response)
                .WithOne(t => t.Ticket)
                .HasForeignKey<Ticket>(t => t.LegalAdviseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(h => h.TicketStatusId)
                .HasDefaultValueSql("1");

            //builder
            //.Property(a => a.TicketId)
            //.IsRequired(false);
        }
    }
}
