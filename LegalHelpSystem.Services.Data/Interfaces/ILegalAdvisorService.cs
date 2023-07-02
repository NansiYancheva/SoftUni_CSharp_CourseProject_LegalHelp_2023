namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;

    public interface ILegalAdvisorService
    {
        Task<bool> LegalAdvisorExistsByUserIdAsync(string userId);

        Task Create(string userId, BecomeLegalAdvisorFormModel model);


    }
}
