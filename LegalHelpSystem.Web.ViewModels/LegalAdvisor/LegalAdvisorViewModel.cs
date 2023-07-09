namespace LegalHelpSystem.Web.ViewModels.LegalAdvisor
{
    using LegalHelpSystem.Web.ViewModels.LegalAdvise;
    public class LegalAdvisorViewModel
    {
        public LegalAdvisorViewModel()
        {
            this.LegalAdvises = new HashSet<LegalAdviseViewModel>();
        }
        //public string Id { get; set; } = null!;

        //public string FullName { get; set; } = null!;

        public IEnumerable<LegalAdviseViewModel> LegalAdvises { get; set; }
    }
}
