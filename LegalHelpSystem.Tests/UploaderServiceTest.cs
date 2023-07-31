namespace LegalHelpSystem.Tests
{
    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Review;
    using System.ComponentModel.DataAnnotations;

    [TestFixture]
    public class UploaderServiceTest
    {
        private LegalHelpDbContext context;
        private IUploaderService uploaderService;

        [SetUp]
        public async Task Setup()
        {
            var uploaderList = new List<Uploader>()
            {
                new Uploader
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("6F740C14-9AB6-4905-B5C1-A000A193B512"),
                    User = new ApplicationUser()
                    {
                        FirstName = "John",
                        LastName = "Wick",
                        Email = "ilovedogs@abv.bg"
                    },
                    UploadedDocuments = new List<Document>()
                        {
                            new Document
                            {
                                Id = Guid.NewGuid(),
                                Name  = "Declaration",
                                DocumentTypeId = 1,
                                AttachedFile = new byte[5],
                                TicketId = Guid.NewGuid()
                            },

                            new Document
                            {
                                Id = Guid.NewGuid(),
                                Name  = "Contract",
                                DocumentTypeId = 2,
                                AttachedFile = new byte[5],
                                TicketId = Guid.NewGuid()
                            },
                        }
                },
                new Uploader
                {
                   Id = Guid.NewGuid(),
                   UserId = Guid.NewGuid(),
                }
            };

            var options = new DbContextOptionsBuilder<LegalHelpDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDb")
               .Options;

            context = new LegalHelpDbContext(options);

            await context.Uploaders.AddRangeAsync(uploaderList);

            await context.SaveChangesAsync();

            uploaderService = new UploaderService(context);
        }



        [Test]
        public async Task AddDocumentToUploaderAsync_ShouldBeDoneProperly()
        {
            var uploader = await context.Uploaders.FirstOrDefaultAsync();
            string uploaderId = uploader.Id.ToString();
            var document = await context.Documents.FirstOrDefaultAsync();
            string documentId = document.Id.ToString();

            var result = uploaderService.AddDocumentToUploaderByIdAsync(uploaderId, documentId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task IfUploaderExists_ReturnsTrue()
        {
            var uploader = await context.Uploaders.FirstOrDefaultAsync();
            string uploaderId = uploader.UserId.ToString();

            bool result = await uploaderService.UploaderExistsByUserIdAsync(uploaderId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetUploaderIdByUserIdAsync_ShouldReturnCorrectStringId()
        {
            var uploader = await context.Uploaders.FirstOrDefaultAsync();
            string uploaderId = uploader.UserId.ToString();

            var result = await uploaderService.GetUploaderIdByUserIdAsync(uploaderId);

            Assert.That(uploader.Id.ToString(), Is.EqualTo(result));
        }

        [Test]
        public async Task GetUploaderIdByUserIdAsync_ShouldReturnNull()
        {
            var result = await uploaderService.GetUploaderIdByUserIdAsync("some fake Id");

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUploaderNameAsync_ShouldReturnCorrectName()
        {
            var uploader = new Uploader
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

            await context.Uploaders.AddAsync(uploader);
            await context.SaveChangesAsync();

            string uploaderName = $"{uploader.User.FirstName} {uploader.User.LastName}";

            string uploaderId = uploader.UserId.ToString();

            var result = await uploaderService.GetUploaderNameAsync(uploaderId);

            Assert.That(uploaderName, Is.EqualTo(result));
        }

        [Test]
        public async Task GetUploadersReviews_ReturnsCorrectValues()
        {

            var uploader = new Uploader
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

            await context.Uploaders.AddAsync(uploader);
            await context.SaveChangesAsync();


            ReviewsViewModel result = await uploaderService.GetUploaderReviews(uploader.UserId.ToString());

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(uploader.Reviews.Select(x => x.TextReview), result.TextReviews);

            int expectedAggTotalStars = uploader.Reviews.Any() ? uploader.Reviews.Sum(x => x.Stars) / uploader.Reviews.Count : 0;
            Assert.AreEqual(expectedAggTotalStars, result.TotalStars);
        }

    }
}
