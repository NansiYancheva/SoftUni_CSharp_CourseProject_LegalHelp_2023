namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;

    public interface ILegalAdvisorService
    {
        //Become
        Task Create(string userId, BecomeLegalAdvisorFormModel model);

        //Add LegalAdvise To LegalAdvisor
        Task AddLegalAdviseToLegalAdvisorByIdAsync(string legalAdvisorId, string legalAdviseId);

        //Common
        Task<string?> GetLegalAdvisorIdByUserIdAsync(string userId);
        Task<bool> LegalAdvisorExistsByUserIdAsync(string userId);

    }
}
