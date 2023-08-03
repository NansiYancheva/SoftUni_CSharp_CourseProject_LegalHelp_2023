using LegalHelpSystem.Web.ViewModels.LegalAdvise;

namespace LegalHelpSystem.Web.Areas.Admin.Services.Interfaces
{
    public interface ILegalAdviseAdminService
    {
        Task DeleteLegalAdviseByIdAsync(string id);
        Task EditLegalAdviseByIdAndFormModelAsync(string id, LegalAdviseFormModel model);
        Task<string> GetLegalAdviseAsResponse(string id);
    }
}
