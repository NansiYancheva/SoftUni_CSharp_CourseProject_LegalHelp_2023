namespace LegalHelpSystem.Tests
{
    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Services.Data;
    using LegalHelpSystem.Data;
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.DocumentType;

    [TestFixture]
    public class DocumentTypeServiceTest
    {
        private LegalHelpDbContext context;
        private IDocumentTypeService documentTypeService;

        [SetUp]
        public async Task Setup()
        {
           
            var options = new DbContextOptionsBuilder<LegalHelpDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            context = new LegalHelpDbContext(options);        

            documentTypeService = new DocumentTypeService(context);
        }

        [Test]
        public async Task GetAllDocumentTypesAsync_ReturnsCorrectViewModels()
        {
            var documentTypesList = new List<DocumentType>()
            {
                new DocumentType
                {
                Id = 1,
                Name = "Declaration from employee"
                },

                new DocumentType
                {
                Id = 2,
                Name = "Labour Contract"
                }
            };

            await context.DocumentTypes.AddRangeAsync(documentTypesList);

            await context.SaveChangesAsync();

            IEnumerable<DocumentSelectTypeFormModel> result = await documentTypeService.AllDocumentTypesAsync();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<DocumentSelectTypeFormModel>>(result);

            foreach (var model in result)
            {
                Assert.IsTrue(model.Id > 0);
                Assert.IsFalse(string.IsNullOrEmpty(model.Name));
            }

            int expectedDocumentTypesCount = await context.DocumentTypes.CountAsync();
            Assert.AreEqual(expectedDocumentTypesCount, result.Count());
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsTrueForExistingDocumentTypeId()
        {
            var documentTypeNew = new DocumentType()
            {
                Id = 3,
                Name = "Declaration from employer"
            };

            await context.DocumentTypes.AddAsync(documentTypeNew);

            await context.SaveChangesAsync();

            var existingDocumentType = await context.DocumentTypes.FirstOrDefaultAsync();
            var existingDocumentTypeId = existingDocumentType.Id;

            bool result = await documentTypeService.ExistsByIdAsync(existingDocumentTypeId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsFalseForNonExistingDocumentTypeId()
        {
            int nonExistingDocumentTypeId = 100;

            bool result = await documentTypeService.ExistsByIdAsync(nonExistingDocumentTypeId);

            Assert.IsFalse(result);
        }
    }
}
