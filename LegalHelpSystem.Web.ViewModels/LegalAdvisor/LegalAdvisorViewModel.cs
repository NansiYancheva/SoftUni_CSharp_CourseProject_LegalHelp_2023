namespace LegalHelpSystem.Web.ViewModels.LegalAdvisor
{
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    public class LegalAdvisorViewModel
    {
        public LegalAdvisorViewModel()
        {
            this.LegalAdvises = new HashSet<LegalAdviseViewModel>();
        }

        public IEnumerable<LegalAdviseViewModel> LegalAdvises { get; set; }
    }
}
