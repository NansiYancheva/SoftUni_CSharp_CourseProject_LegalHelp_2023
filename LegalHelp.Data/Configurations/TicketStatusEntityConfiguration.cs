namespace LegalHelpSystem.Data.Configurations
{ 
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;


    public class TicketStatusEntityConfiguration : IEntityTypeConfiguration<TicketStatus>
    {
        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.HasData(this.GenerateTicketStatus());
        }

        private TicketStatus[] GenerateTicketStatus()
        {
            ICollection<TicketStatus> ticketStatuses = new HashSet<TicketStatus>();

            TicketStatus ticketStatus;
            ticketStatus = new TicketStatus()
            {
                Id = 1,
                Name = "In process of response from legal advisor"
            };
            ticketStatuses.Add(ticketStatus);

            ticketStatus = new TicketStatus()
            {
                Id = 2,
                Name = "Resolved"
            };
            ticketStatuses.Add(ticketStatus);

            return ticketStatuses.ToArray();
        }
    }
}
