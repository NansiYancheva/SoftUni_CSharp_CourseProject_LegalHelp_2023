namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.TicketCategory;

    public interface ITicketCategoryService
    {
        Task<IEnumerable<TicketSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
