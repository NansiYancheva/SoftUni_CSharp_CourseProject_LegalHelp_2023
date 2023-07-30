namespace LegalHelpSystem.Tests
{
    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using LegalHelpSystem.Web.ViewModels.Review;

    [TestFixture]
    public class LegalAdvisorServiceTest
    {
        private LegalHelpDbContext context;
        private ILegalAdvisorService legalAdvisorService;

        [SetUp]
        public async Task Setup()
        {
            var legalAdvisors = new List<LegalAdvisor>()
            {
                new LegalAdvisor
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("6F740C14-9AB6-4905-B5C1-A000A193B512"),
                    User = new ApplicationUser()
                    {
                        FirstName = "John",
                        LastName = "Wick",
                        Email = "ilovedogs@abv.bg"
                    },

                    LegalAdvises = new List<LegalAdvise>()
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
                        }
                },
                new LegalAdvisor
                {
                   Id = Guid.NewGuid(),
                   UserId = Guid.NewGuid(),
                }
            };

            var options = new DbContextOptionsBuilder<LegalHelpDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            context = new LegalHelpDbContext(options);

            await context.LegalAdvisors.AddRangeAsync(legalAdvisors);

            await context.SaveChangesAsync();

            legalAdvisorService = new LegalAdvisorService(context);
        }



        [Test]
        public async Task AddLegalAdviseToLegalAdvisorAsync_ShouldBeDoneProperly()
        {
            var legalAdvisor = await context.LegalAdvisors.FirstOrDefaultAsync();
            string legalAdvisorId = legalAdvisor.Id.ToString();
            var legalAdvise = await context.LegalAdvises.FirstOrDefaultAsync();
            string legalAdviseId = legalAdvise.Id.ToString();

            var result = legalAdvisorService.AddLegalAdviseToLegalAdvisorByIdAsync(legalAdvisorId, legalAdviseId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task IfLegalAdvisorExists_ReturnsTrue()
        {
            var legalAdvisor = await context.LegalAdvisors.FirstOrDefaultAsync();
            string legalAdvisorId = legalAdvisor.UserId.ToString();

            bool result = await legalAdvisorService.LegalAdvisorExistsByUserIdAsync(legalAdvisorId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetLegalAdvisorIdByUserIdAsync_ShouldReturnCorrectStringId()
        {
            var legalAdvisor = await context.LegalAdvisors.FirstOrDefaultAsync();
            string legalAdvisorId = legalAdvisor.UserId.ToString();

            var result = await legalAdvisorService.GetLegalAdvisorIdByUserIdAsync(legalAdvisorId);

            Assert.That(legalAdvisor.Id.ToString(), Is.EqualTo(result));
        }

        [Test]
        public async Task GetLegalAdvisorIdByUserIdAsync_ShouldReturnNull()
        { 
            var result = await legalAdvisorService.GetLegalAdvisorIdByUserIdAsync("some fake Id");

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLegalAdvisorNameAsync_ShouldReturnCorrectName()
        {
            var legalAdvisor = new LegalAdvisor
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("6F740C14-9AB6-4905-B5C1-A000A193B512"),
                User = new ApplicationUser()
                {
                    FirstName = "John",
                    LastName = "Wick",
                    Email = "ilovedogs@abv.bg"
                },
            };

            await context.LegalAdvisors.AddAsync(legalAdvisor);
            await context.SaveChangesAsync();

            string legalAdvisorName = $"{legalAdvisor.User.FirstName} {legalAdvisor.User.LastName}";

            string legalAdvisorId = legalAdvisor.UserId.ToString();

            var result = await legalAdvisorService.GetLegalAdvisorNameAsync(legalAdvisorId);

            Assert.That(legalAdvisorName, Is.EqualTo(result));
        }

        [Test]
        public async Task GetLegalAdvisorReviews_ReturnsCorrectValues()
        {

            var legalAdvisor = new LegalAdvisor
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("6F740C14-9AB6-4905-B5C1-A000A193B512"),
                Reviews = new List<Review>
                {
                    new Review { TextReview = "Review 1", Stars = 5 },
                    new Review { TextReview = "Review 2", Stars = 3 }
                },
                 User = new ApplicationUser()
                 {
                     FirstName = "John",
                     LastName = "Wick",
                     Email = "ilovedogs@abv.bg"
                 },
            };

            await context.LegalAdvisors.AddAsync(legalAdvisor);
            await context.SaveChangesAsync();


            ReviewsViewModel result = await legalAdvisorService.GetLegalAdvisorReviews(legalAdvisor.UserId.ToString());

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(legalAdvisor.Reviews.Select(x => x.TextReview), result.TextReviews);

            int expectedAggTotalStars = legalAdvisor.Reviews.Any() ? legalAdvisor.Reviews.Sum(x => x.Stars) / legalAdvisor.Reviews.Count : 0;
            Assert.AreEqual(expectedAggTotalStars, result.TotalStars);
        }

    }
}
