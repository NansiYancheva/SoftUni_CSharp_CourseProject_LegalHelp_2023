namespace LegalHelpSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LegalHelpSystem.Data.Models;

    public class TicketCategoryEntityConfiguration : IEntityTypeConfiguration<TicketCategory>
    {
        public void Configure(EntityTypeBuilder<TicketCategory> builder)
        {
            builder.HasData(this.GenerateTicketCategory());
        }

        private TicketCategory[] GenerateTicketCategory()
        {
            ICollection<TicketCategory> ticketCategories = new HashSet<TicketCategory>();

            TicketCategory ticketCategory;
            ticketCategory = new TicketCategory()
            {
                Id = 1,
                Name = "Request for document"
            };
            ticketCategories.Add(ticketCategory);

            ticketCategory = new TicketCategory()
            {
                Id = 2,
                Name = "Request for advise"
            };
            ticketCategories.Add(ticketCategory);

            return ticketCategories.ToArray();
        }
    }
}
