﻿namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Ticket;


    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Web.Infrastructure.Extensions;

    public class TicketController : BaseController
    {
        private readonly ITicketCategoryService ticketCategoryService;
        private readonly ITicketService ticketService;

        public TicketController(ITicketCategoryService _ticketCategoryService, ITicketService _ticketService)
        {
            this.ticketCategoryService = _ticketCategoryService;
            this.ticketService = _ticketService;
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
                return View("Error");
                //  return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketAddOrEditFormModel model)
        {
            bool categoryExists =
                await this.ticketCategoryService.ExistsByIdAsync(model.TicketCategoryId);
            if (!categoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
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
                string ticketId = await ticketService.AddTicketAsync(model, userId);

                this.TempData[SuccessMessage] = "Ticket was added successfully!";
                //to be changed = My/All, Tickets
                //return this.RedirectToAction("Details", "House", new { id = houseId });
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your request! Please try again later or contact administrator!");
                model.TicketCategories = await this.ticketCategoryService.AllCategoriesAsync();

                return this.View(model);
            }
        }
    }
}
