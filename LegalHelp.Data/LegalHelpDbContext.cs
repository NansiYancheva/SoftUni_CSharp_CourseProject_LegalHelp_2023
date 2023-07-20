namespace LegalHelpSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    using LegalHelpSystem.Data.Models;

    public class LegalHelpDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public LegalHelpDbContext(DbContextOptions<LegalHelpDbContext> options)
            : base(options)
        {
        }
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public DbSet<LegalAdvise> LegalAdvises { get; set; } = null!;
        public DbSet<LegalAdvisor> LegalAdvisors { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<TicketCategory> TicketCategories { get; set; } = null!;
        public DbSet<Uploader> Uploaders { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(LegalHelpDbContext)) ??
                                      Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}