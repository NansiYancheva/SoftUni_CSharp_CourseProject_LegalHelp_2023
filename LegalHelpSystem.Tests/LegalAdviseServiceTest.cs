namespace LegalHelpSystem.Tests
{
    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;

    [TestFixture]
    public class LegalAdviseServiceTests
    {
        private LegalHelpDbContext context;
        private ILegalAdviseService legalAdviseService;

        [SetUp]
        public async Task Setup()
        {
            var legalAdvisesList = new List<LegalAdvise>()
            {
                new LegalAdvise
                {
                    Id = Guid.NewGuid(),
                    AdviseResponse = "Some legal advise which will help",
                    TicketId = Guid.NewGuid()
                },

                new LegalAdvise
                {
                    Id = Guid.NewGuid(),
                    AdviseResponse = "Some legal advise",
                    TicketId = Guid.Parse("7b8ceacd-4250-4f27-84fb-315027936ad1"),
                    LegalAdvisorId = Guid.Parse("701ea5db-eaf3-4ef2-8a4c-6c5b7f3c34f9")
                },
            };

            var options = new DbContextOptionsBuilder<LegalHelpDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            context = new LegalHelpDbContext(options);

            await context.LegalAdvises.AddRangeAsync(legalAdvisesList);
            await context.SaveChangesAsync();

            legalAdviseService = new LegalAdviseService(context);
        }

        [Test]
        public async Task AddLegalAdviseAsync_ShouldCreateLegalAdviseProperly()
        {
            // Arrange
            var legalAdvisorId = "701ea5db-eaf3-4ef2-8a4c-6c5b7f3c34f9";
            var formModel = new LegalAdviseFormModel
            {
                AdviseResponse = "Some legal advise",
                TicketId = "7b8ceacd-4250-4f27-84fb-315027936ad1",
                TicketSubject = "About employment issue",
                TicketDescription = "I have problem with my employer"
            };

            await legalAdviseService
                .AddLegalAdviseAsync(formModel, legalAdvisorId);


            var actualEntity = await context.LegalAdvises
                .Where(x => x.AdviseResponse == formModel.AdviseResponse)
                .Where(x => x.TicketId.ToString() == formModel.TicketId)
                .Where(x => x.LegalAdvisorId.ToString() == legalAdvisorId)
                .FirstOrDefaultAsync();

            Assert.That(actualEntity, Is.Not.Null);
        }
    }
}
