﻿namespace LegalHelpSystem.Web.ViewModels.LegalAdvise
{
    using LegalHelpSystem.Data.Models;
    public class LegalAdviseViewModel
    {
        public string Id { get; set; } = null!;

        public string AdviseResponse { get; set; } = null!;

        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; } = null!;


        public Guid LegalAdvisorId { get; set; }

        public virtual LegalAdvisor LegalAdvisor { get; set; } = null!;
    }
}