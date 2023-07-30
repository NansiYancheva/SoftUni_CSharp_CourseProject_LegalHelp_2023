namespace LegalHelpSystem.Tests
{
    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using LegalHelpSystem.Web.ViewModels.Review;

    [TestFixture]
    public class LegalAdviseServiceTest
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

            var testUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Wick",
                Email = "ilovedogs@abv.bg"
            };


            var options = new DbContextOptionsBuilder<LegalHelpDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            context = new LegalHelpDbContext(options);

            await context.LegalAdvises.AddRangeAsync(legalAdvisesList);

            await context.Users.AddAsync(testUser);


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

        [Test]
        public async Task AllByUserIdAsync_ShouldReturnCorrectLAByUserIdCount()
        {
            var testUser = await context.Users.FirstOrDefaultAsync();
            string userId = testUser.Id.ToString();

            IEnumerable<LegalAdviseViewModel> result = await legalAdviseService.AllByUserIdAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<LegalAdviseViewModel>>(result);

            var legalAdvisesForUser = await context.LegalAdvises
                .Where(la => la.Ticket.UserId.ToString() == userId)
                .ToListAsync();

            Assert.AreEqual(legalAdvisesForUser.Count, result.Count());
        }

        [Test]
        public async Task IfLegalAdviseExists_ReturnsTrue()
        {
            var existingLegalAdvise = new LegalAdvise
            {
                Id = Guid.NewGuid(),
                AdviseResponse = "Some legal advise which will help",
                TicketId = Guid.NewGuid()
            };

            await context.LegalAdvises.AddAsync(existingLegalAdvise);
            await context.SaveChangesAsync();

            bool result = await legalAdviseService.LegalAdviseExistsByIdAsync(existingLegalAdvise.Id.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetLegalAdviseResponseAsync_ReturnsCorrectResponseForExistingId()
        {
            var existingLegalAdvise = new LegalAdvise
            {
                Id = Guid.NewGuid(),
                AdviseResponse = "Some legal advise which will help",
                TicketId = Guid.NewGuid()
            };

            await context.LegalAdvises.AddAsync(existingLegalAdvise);
            await context.SaveChangesAsync();

            string result = await legalAdviseService.GetLegalAdviseResponseAsync(existingLegalAdvise.Id.ToString());

            Assert.AreEqual(existingLegalAdvise.AdviseResponse, result);
        }

        [Test]
        public async Task GetLegalAdviseReviews_ReturnsCorrectValues()
        {

            var legalAdviseId = Guid.NewGuid();
            var legalAdvise = new LegalAdvise
            {
                Id = legalAdviseId,
                AdviseResponse = "Some legal advise response",
                Reviews = new List<Review>
            {
                new Review { TextReview = "Review 1", Stars = 5 },
                new Review { TextReview = "Review 2", Stars = 3 }
            },
                TicketId = Guid.NewGuid()
            };

            await context.LegalAdvises.AddAsync(legalAdvise);
            await context.SaveChangesAsync();


            ReviewsViewModel result = await legalAdviseService.GetLegalAdviseReviews(legalAdviseId.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(legalAdvise.AdviseResponse, result.Object);

            CollectionAssert.AreEquivalent(legalAdvise.Reviews.Select(x => x.TextReview), result.TextReviews);

            int expectedAggTotalStars = legalAdvise.Reviews.Any() ? legalAdvise.Reviews.Sum(x => x.Stars) / legalAdvise.Reviews.Count : 0;
            Assert.AreEqual(expectedAggTotalStars, result.TotalStars);
        }


        [Test]
        public async Task GetAllLegalAdvisesAsync_ReturnsCorrectViewModels()
        {
           var legalAdvisesList = new List<LegalAdvise>
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
                   AdviseResponse = "Some legal advise which will help",
                   TicketId = Guid.NewGuid()
                  }
              };

            await context.LegalAdvises.AddRangeAsync(legalAdvisesList);
            await context.SaveChangesAsync();

            IEnumerable<LegalAdviseViewModel> result = await legalAdviseService.GetAllLegalAdvisesAsync();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<LegalAdviseViewModel>>(result);

        }

        [Test]
        public async Task GetAllLegalAdvisesAsync_ReturnsEmptyListWhenNoSubjects()
        {
            var legalAdvisesList = new List<LegalAdvise>
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
                      AdviseResponse = "Some legal advise which will help",
                      TicketId = Guid.NewGuid()
                    }
                };

            await context.LegalAdvises.AddRangeAsync(legalAdvisesList);
            await context.SaveChangesAsync();

            IEnumerable<LegalAdviseViewModel> result = await legalAdviseService.GetAllLegalAdvisesAsync();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<LegalAdviseViewModel>>(result);
            Assert.IsEmpty(result);
        }



    }
}


