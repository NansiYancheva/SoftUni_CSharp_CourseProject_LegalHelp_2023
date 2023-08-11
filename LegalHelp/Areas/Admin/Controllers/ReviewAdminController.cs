namespace LegalHelpSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;
    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Review;

    using static Common.NotificationMessagesConstants;
    using Microsoft.CodeAnalysis;


    public class ReviewAdminController : BaseAdminController
    {
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ILegalAdvisorAdminService legalAdvisorAdminService;
        private readonly ILegalAdviseService legalAdviseService;
        private readonly ILegalAdviseAdminService legalAdviseAdminService;
        private readonly IUploaderService uploaderService;
        private readonly IUploaderAdminService uploaderAdminService;
        private readonly IDocumentService documentService;
        private readonly IDocumentAdminService documentAdminService;
        private readonly IReviewAdminService reviewAdminService;

        public ReviewAdminController(ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService, IUploaderService _uploaderService, IDocumentService _documentService, IReviewAdminService _reviewAdminService, IUploaderAdminService _uploaderAdminService, ILegalAdvisorAdminService legalAdvisorAdminService, ILegalAdviseAdminService _legalAdviseAdminService, IDocumentAdminService _documentAdminService)
        {
            this.legalAdvisorService = _legalAdvisorService;
            this.legalAdvisorAdminService = legalAdvisorAdminService;
            this.legalAdviseService = _legalAdviseService;
            this.legalAdviseAdminService = _legalAdviseAdminService;
            this.uploaderService = _uploaderService;
            this.uploaderAdminService = _uploaderAdminService;
            this.documentService = _documentService;
            this.documentAdminService = _documentAdminService;
            this.reviewAdminService = _reviewAdminService;
        }


        [Route("ReviewAdmin/DeleteReview")]
            //Delete
            [HttpGet]
            public async Task<IActionResult> DeleteReview(string objectId, string textReview)
            {
            if (objectId == null)
            {
                this.TempData[ErrorMessage] = "Object with such id does not exists!";

                return this.RedirectToAction("All", "Tickets", new { Area = "" });
            }
            //check type of object
            bool isUploader =
                   await this.uploaderService
                   .UploaderExistsByUserIdAsync(objectId);
            bool isLegalAdvisor =
               await this.legalAdvisorService
               .LegalAdvisorExistsByUserIdAsync(objectId);
            bool legalAdviseExists =
               await this.legalAdviseService
               .LegalAdviseExistsByIdAsync(objectId);
            bool documentExists =
               await this.documentService
               .DocumentExistsByIdAsync(objectId);

            if (!isLegalAdvisor && !isUploader && !documentExists && !legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "There is no object for review!";

                return this.RedirectToAction("All", "Ticket", new { Area = "" });
            }

            try
            {
                ReviewFormModel reviewFormModel = new ReviewFormModel();
                reviewFormModel.ObjectId = objectId;
                reviewFormModel.TextReview = textReview;

                if (isUploader)
                {
                    reviewFormModel.Object = await this.uploaderService
                        .GetUploaderNameAsync(objectId);

                }
                else if (isLegalAdvisor)
                {
                    reviewFormModel.Object = await this.legalAdvisorService
                        .GetLegalAdvisorNameAsync(objectId);
                }
                else if (documentExists)
                {
                    reviewFormModel.Object = await this.documentService
                        .GetDocumentNameAsync(objectId);
                }
                else if (legalAdviseExists)
                {
                    reviewFormModel.Object = await this.legalAdviseService
                        .GetLegalAdviseResponseAsync(objectId);
                }

                return View(reviewFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }


        [Route("ReviewAdmin/DeleteReview")]
        //Delete        
        [HttpPost]
        public async Task<IActionResult> DeleteReview(string objectId, ReviewFormModel model)
        {
            if (objectId == null)
            {
                this.TempData[ErrorMessage] = "Object with such id does not exists!";

                return this.RedirectToAction("All", "Tickets", new { Area = "" });
            }
            //check type of object
            bool isUploader =
                   await this.uploaderService
                   .UploaderExistsByUserIdAsync(objectId);
            bool isLegalAdvisor =
               await this.legalAdvisorService
               .LegalAdvisorExistsByUserIdAsync(objectId);
            bool legalAdviseExists =
               await this.legalAdviseService
               .LegalAdviseExistsByIdAsync(objectId);
            bool documentExists =
               await this.documentService
               .DocumentExistsByIdAsync(objectId);

            if (!isLegalAdvisor && !isUploader && !documentExists && !legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "There is no object for review!";

                return this.RedirectToAction("All", "Ticket", new { Area = "" });
            }

            try
            {
                if (isUploader)
                {
                    await this.reviewAdminService
                              .DeleteSingleReviewItSelfByUploaderIdAsync(objectId, model.TextReview);
                    await this.uploaderAdminService
                              .RemoveSingleReviewFromListOfReviewsOfUploaderAsync(objectId, model.TextReview);
                }
                else if (isLegalAdvisor)
                {
                    await this.reviewAdminService
                              .DeleteSingleReviewItSelfByLegalAdvisorIdAsync(objectId, model.TextReview);
                    await this.legalAdvisorAdminService
                              .RemoveSingleReviewFromListOfReviewsOfLegalAdvisorAsync(objectId, model.TextReview);
                }
                else if (legalAdviseExists)
                {
                    await this.reviewAdminService
                              .DeleteSingleReviewItSelfByLegalAdviseIdAsync(objectId, model.TextReview);
                    await this.legalAdviseAdminService
                              .RemoveSingleReviewFromListOfReviewsOfLegalAdviseAsync(objectId, model.TextReview);
                }
                else if (documentExists)
                {
                    await this.reviewAdminService
                              .DeleteSingleReviewItSelfByDocumentIdAsync(objectId, model.TextReview);
                    await this.documentAdminService
                              .RemoveSingleReviewFromListOfReviewsOfDocumentAsync(objectId, model.TextReview);
                }



                this.TempData[WarningMessage] = "The review was successfully deleted!";
                return this.RedirectToAction("All", "Ticket", new { Area = "" });
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
    }
}
