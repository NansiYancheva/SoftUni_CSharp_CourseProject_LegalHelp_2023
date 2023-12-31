﻿namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Ticket;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using LegalHelpSystem.Web.Areas.Admin.Services.Interfaces;

    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Web.Areas.Admin.Services;
    using LegalHelpSystem.Data.Models;


    public class TicketController : BaseController
    {
        private readonly ITicketCategoryService ticketCategoryService;
        private readonly ITicketService ticketService;
        private readonly ITicketAdminService ticketAdminService;
        private readonly IDocumentAdminService documentAdminService;
        private readonly IReviewAdminService reviewAdminService;
        private readonly ILegalAdviseAdminService legalAdviseAdminService;

        public TicketController(ITicketCategoryService _ticketCategoryService, ITicketService _ticketService, ITicketAdminService _ticketAdminService, IDocumentAdminService _documentAdminService, IReviewAdminService _reviewAdminService, ILegalAdviseAdminService _legalAdviseAdminService)
        {
            this.ticketCategoryService = _ticketCategoryService;
            this.ticketService = _ticketService;
            this.ticketAdminService = _ticketAdminService;
            this.documentAdminService = _documentAdminService;
            this.reviewAdminService = _reviewAdminService;
            this.legalAdviseAdminService = _legalAdviseAdminService;
        }
        //Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                TicketAddOrEditFormModel formModel = new TicketAddOrEditFormModel()
                {
                    TicketCategories = await this.ticketCategoryService.AllCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {               
                return this.GeneralError();
            }
        }
        //Add
        [HttpPost]
        public async Task<IActionResult> Add(TicketAddOrEditFormModel model)
        {
            bool categoryExists =
                await this.ticketCategoryService.ExistsByIdAsync(model.TicketCategoryId);
            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(model.TicketCategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? userId = this.User.GetId();
                string ticketId = await ticketService.AddTicketAsync(model, userId!);

                this.TempData[SuccessMessage] = "Ticket was added successfully!";
                return this.RedirectToAction("All", "Ticket");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your request! Please try again later or contact administrator!");
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }
        }
        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to edit or an admin!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            bool resolvedStatus = await this.ticketService
                 .ResolvedTicket(id);
            if (resolvedStatus == true && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id was already resolved and cannot be edited!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            try
            {
                TicketAddOrEditFormModel formModel = await this.ticketService
                    .GetTicketForEditByIdAsync(id);
                formModel.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {             
                 return this.GeneralError();
            }
        }
        //Edit
        [HttpPost]
        public async Task<IActionResult> Edit(string id, TicketAddOrEditFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }

            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to edit or an admin!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            bool resolvedStatus = await this.ticketService
                  .ResolvedTicket(id);
            if (resolvedStatus == true && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id was already resolved and cannot be edited!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            try
            {
                await this.ticketService.EditTicketByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty,
                    "Unexpected error occurred while trying to edit the ticket. Please try again later or contact administrator!");
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Ticket was edited successfully!";
            return this.RedirectToAction("Mine", "Ticket");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            try
            {
                TicketPerDeleteFormModel viewModel =
                    await this.ticketService.GetTicketForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {            
                return this.GeneralError();
            }
        }
        //Delete        
        [HttpPost]
        public async Task<IActionResult> Delete(string id, TicketPerDeleteFormModel model)
        {
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            try
            {
                if (model.ResolvedTicketStatus == true)
                {
                    if (model.DocumentName != null)
                    {
                        string documentId = 
                         await this.documentAdminService
                                   .FindDocumentIdByTicketIdAsync(id);
                        await this.ticketAdminService
                                  .RemoveDocumentFromTicket(id);
                        await this.reviewAdminService
                                  .DeleteTheReviewItSelfByDocumentIdAsync(documentId);
                        await this.documentAdminService
                                  .RemoveReviewsOfDocumentAsync(documentId);
                        await this.documentAdminService
                                  .DeleteDocumentByIdAsync(documentId);
                    }
                    else if (model.Response != null)
                    {
                        string legalAdviseId =
                        await this.legalAdviseAdminService
                                  .FindLegalAdviseIdByTicketIdAsync(id);
                        //first remove legalAdviseIdFromTicket
                        await this.ticketAdminService
                                  .RemoveLegalAdviseFromTicket(id);
                        //delete the review by legal advise id
                        await this.reviewAdminService
                                  .DeleteTheReviewItSelfByLegalAdviseIdAsync(legalAdviseId);
                        //remove the reviews
                        await this.legalAdviseAdminService
                                  .RemoveReviewsOfLegalAdviseAsync(legalAdviseId);
                        //After that delete the legalAdvise itself
                        await this.legalAdviseAdminService
                                  .DeleteLegalAdviseByIdAsync(legalAdviseId);
                    }
                }
                await this.ticketService
                          .DeleteTicketByIdAsync(id);

                this.TempData[WarningMessage] = "The ticket was successfully deleted!";
                return this.RedirectToAction("Mine", "Ticket");
            }
            catch (Exception)
            {
               return this.GeneralError();
            }
        }

        //Mine
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<TicketAllViewModel> myTickets =
                new List<TicketAllViewModel>();

            string userId = this.User.GetId()!;

            try
            {
                if (User.IsAdmin())
                {
                    return RedirectToAction("All", "Ticket");
                }
                else
                {
                    myTickets.AddRange(await this.ticketService.AllByUserIdAsync(userId!));
                }
              
                return this.View(myTickets);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [AllowAnonymous]
        //All - get
        public async Task<IActionResult> All()
        {
                IEnumerable<TicketAllViewModel> modelByUser = await ticketService.GetAllTicketsAsync();
                return View(modelByUser);
        }

        //Sort - all
        [HttpPost]
        public async Task<IActionResult> SortAllTickets(string sortOrder)
        {
            IEnumerable<TicketAllViewModel> getAllTicketsModel = await ticketService.GetAllTicketsAsync();
            if (sortOrder == "desc")
            {
                getAllTicketsModel = getAllTicketsModel.OrderBy(t => t.ResolvedTicketStatus).ToList();
            }
            else if (sortOrder == "asc")
            {
                getAllTicketsModel = getAllTicketsModel.OrderByDescending(t => t.ResolvedTicketStatus).ToList();
            }
            return View(getAllTicketsModel);
        }

        //Sort - mine
        [HttpPost]
        public async Task<IActionResult> SortMineTickets(string sortOrder)
        {
            List<TicketAllViewModel> myTickets =
               new List<TicketAllViewModel>();

            string userId = this.User.GetId()!;

            try
            {
                myTickets.AddRange(await this.ticketService.AllByUserIdAsync(userId!));
                if (sortOrder == "desc")
                {
                    myTickets = myTickets.OrderBy(t => t.ResolvedTicketStatus).ToList();
                }
                else if (sortOrder == "asc")
                {
                    myTickets = myTickets.OrderByDescending(t => t.ResolvedTicketStatus).ToList();
                }
                return View(myTickets);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

    }

}

