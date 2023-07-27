namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    public class LegalAdviseViewModel
    {
        public string Id { get; set; } = null!;

        public string AdviseResponse { get; set; } = null!;

        public string TicketSubject { get; set; } = null!;

        public string TicketDescription { get; set; } = null!;

        public string LegalAdvisorName { get; set; } = null!;

        public string LegalAdvisorUserId { get; set; } = null!;

    }
}
