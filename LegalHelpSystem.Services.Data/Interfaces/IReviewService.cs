﻿namespace LegalHelpSystem.Services.Data.Interfaces
{
    using LegalHelpSystem.Web.ViewModels.Review;
    using System;
    using System.Collections.Generic;

    public interface IReviewService
    {
        Task AddDocumentReview(ReviewFormModel model, string userId);
        Task AddLegalAdviseReview(ReviewFormModel model, string userId);
        Task AddLegalAdvisorReview(ReviewFormModel model, string userId);
        Task AddUploaderReview(ReviewFormModel model, string userId);
    }
}
