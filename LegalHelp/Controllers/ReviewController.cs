namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.ViewModels.Review;

    using static Common.NotificationMessagesConstants;

    public class ReviewController : BaseController
    {
        private readonly ITicketService ticketService;
        private readonly ILegalAdvisorService legalAdvisorService;
        private readonly ILegalAdviseService legalAdviseService;
        private readonly IUploaderService uploaderService;
        private readonly IDocumentService documentService;
        private readonly IReviewService reviewService;

        public ReviewController(ITicketService _ticketService, ILegalAdvisorService _legalAdvisorService, ITicketCategoryService _ticketCategoryService, ILegalAdviseService _legalAdviseService, IUploaderService _uploaderService, IDocumentService _documentService, IReviewService _reviewService)
        {
            this.ticketService = _ticketService;
            this.legalAdvisorService = _legalAdvisorService;
            this.legalAdviseService = _legalAdviseService;
            this.uploaderService = _uploaderService;
            this.documentService = _documentService;
            this.reviewService = _reviewService;
           
        }
        //Add
        [HttpGet]
        public async Task<IActionResult> Add(string objectId)
        {
            //check type of object
            bool isUploader =
                   await this.uploaderService.UploaderExistsByUserIdAsync(objectId);
            bool isLegalAdvisor =
               await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(objectId);
            bool legalAdviseExists =
               await this.legalAdviseService.LegalAdviseExistsByIdAsync(objectId);
            bool documentExists =
               await this.documentService.DocumentExistsByIdAsync(objectId);

            if (!isLegalAdvisor && !isUploader && !documentExists && !legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "There is no object for review!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            try
            {
                ReviewFormModel reviewFormModel = new ReviewFormModel();
                reviewFormModel.ObjectId = objectId;

                if (isUploader)
                {
                    reviewFormModel.Object = await this.uploaderService.GetUploaderNameAsync(objectId);
                   
                }
                else if(isLegalAdvisor)
                {
                    reviewFormModel.Object = await this.legalAdvisorService.GetLegalAdvisorNameAsync(objectId);
                }
                else if(documentExists)
                {
                    reviewFormModel.Object = await this.documentService.GetDocumentNameAsync(objectId);
                }
                else if(legalAdviseExists)
                {
                    reviewFormModel.Object = await this.legalAdviseService.GetLegalAdviseResponseAsync(objectId);
                }

                return View(reviewFormModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        //Add
        [HttpPost]
        public async Task<IActionResult> Add(ReviewFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            bool isUploader =
        await this.uploaderService.UploaderExistsByUserIdAsync(model.ObjectId);
            bool isLegalAdvisor =
               await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(model.ObjectId);
            bool legalAdviseExists =
               await this.legalAdviseService.LegalAdviseExistsByIdAsync(model.ObjectId);
            bool documentExists =
               await this.documentService.DocumentExistsByIdAsync(model.ObjectId);

            if (!isLegalAdvisor && !isUploader && !documentExists && !legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "There is no object for review!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            try
            {
                string userId = this.User.GetId()!;
                //to create the review itself in the database
                //to add review to the object
                if (legalAdviseExists)
                {
                    await this.reviewService.AddLegalAdviseReview(model, userId);
                }
                //string? legalAdvisorId = await this.legalAdvisorService.GetLegalAdvisorIdByUserIdAsync(this.User.GetId()!);

                //string legalAdviseId = await legalAdviseService.AddLegalAdviseAsync(model, legalAdvisorId!);

                //await this.ticketService.AddLegalAdviseToTicketByIdAsync(model.TicketId, legalAdviseId);

                //await this.legalAdvisorService.AddLegalAdviseToLegalAdvisorByIdAsync(legalAdvisorId!, legalAdviseId);



            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Review was added successfully!";

            return this.RedirectToAction("Mine", "Ticket");
        }



        ////////TeamForReview///////
        
        //ViewReview
        [HttpGet]
        public async Task<IActionResult> View(string id)
        {
            //check type of object
            bool isUploader =
                   await this.uploaderService.UploaderExistsByUserIdAsync(id);
            bool isLegalAdvisor =
               await this.legalAdvisorService.LegalAdvisorExistsByUserIdAsync(id);
            bool legalAdviseExists =
               await this.legalAdviseService.LegalAdviseExistsByIdAsync(id);
            bool documentExists =
               await this.documentService.DocumentExistsByIdAsync(id);

            if (!isLegalAdvisor && !isUploader && !documentExists && !legalAdviseExists)
            {
                this.TempData[ErrorMessage] = "There is no object to view review of!";

                return this.RedirectToAction("All", "Ticket");
            }

            try
            {

                ReviewsViewModel reviewViewModel = new ReviewsViewModel();
               //? reviewViewModel.ObjectId = id;

                if (isUploader)
                {
                    reviewViewModel = await this.uploaderService.GetUploaderReviews(id);

                }
                else if (isLegalAdvisor)
                {
                    reviewViewModel = await this.legalAdvisorService.GetLegalAdvisorReviews(id);
                }
                else if (documentExists)
                {
                    reviewViewModel = await this.documentService.GetDocumentReviews(id);
                }
                else if (legalAdviseExists)
                {
                    reviewViewModel = await this.legalAdviseService.GetLegalAdviseReviews(id);
                }

                return View(reviewViewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
    }
}
