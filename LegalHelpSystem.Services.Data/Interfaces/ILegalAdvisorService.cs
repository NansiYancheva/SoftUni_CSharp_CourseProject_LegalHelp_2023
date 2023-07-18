namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Data.Models;
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    using LegalHelpSystem.Web.ViewModels.LegalAdvisor;
    using System.Collections.Generic;

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
