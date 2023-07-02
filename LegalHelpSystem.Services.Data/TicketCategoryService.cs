namespace LegalHelpSystem.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using LegalHelpSystem.Data;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.TicketCategory;

    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly LegalHelpDbContext dbContext;

        public TicketCategoryService(LegalHelpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TicketSelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<TicketSelectCategoryFormModel> allCategories = await this.dbContext
                .TicketCategories
                .AsNoTracking()
                .Select(c => new TicketSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .TicketCategories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
