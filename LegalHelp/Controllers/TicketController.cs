namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Ticket;


    using static Common.NotificationMessagesConstants;
    using LegalHelpSystem.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;

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
        //Add
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
        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool ticketExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!ticketExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                //return this.RedirectToAction("All"/Mine, "Ticket");
                return this.RedirectToAction("Index", "Home");
            }
            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter)
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to edit!";

                //return this.RedirectToAction("Mine", "House");
                return this.RedirectToAction("Index", "Home");
            }

            bool resolvedStatus = await this.ticketService
                 .ResolvedTicket(id);
            if (resolvedStatus == true)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id was already resolved and cannot be edited!";

                //return this.RedirectToAction("Add", "Ticket");
                return this.RedirectToAction("Index", "Home");
            }
            //to be removed
            return this.RedirectToAction("Index", "Home");
            //        try
            //        {
            //            HouseFormModel formModel = await this.houseService
            //                .GetHouseForEditByIdAsync(id);
            //            formModel.Categories = await this.categoryService.AllCategoriesAsync();

            //            return this.View(formModel);
            //        }
            //        catch (Exception)
            //        {
            //            return this.GeneralError();
            //        }
            //    }

            //    //Edit
            //    [HttpPost]
            //    public async Task<IActionResult> Edit(string id, HouseFormModel model)
            //    {
            //        if (!this.ModelState.IsValid)
            //        {
            //            model.Categories = await this.categoryService.AllCategoriesAsync();

            //            return this.View(model);
            //        }

            //        bool houseExists = await this.houseService
            //            .ExistsByIdAsync(id);
            //        if (!houseExists)
            //        {
            //            this.TempData[ErrorMessage] = "House with the provided id does not exist!";

            //            return this.RedirectToAction("All", "House");
            //        }

            //        bool isUserAgent = await this.agentService
            //            .AgentExistsByUserIdAsync(this.User.GetId()!);
            //        if (!isUserAgent)
            //        {
            //            this.TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

            //            return this.RedirectToAction("Become", "Agent");
            //        }

            //        string? agentId =
            //            await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);
            //        bool isAgentOwner = await this.houseService
            //            .IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);
            //        if (!isAgentOwner)
            //        {
            //            this.TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";

            //            return this.RedirectToAction("Mine", "House");
            //        }

            //        try
            //        {
            //            await this.houseService.EditHouseByIdAndFormModelAsync(id, model);
            //        }
            //        catch (Exception)
            //        {
            //            this.ModelState.AddModelError(string.Empty,
            //                "Unexpected error occurred while trying to update the house. Please try again later or contact administrator!");
            //            model.Categories = await this.categoryService.AllCategoriesAsync();

            //            return this.View(model);
            //        }

            //        this.TempData[SuccessMessage] = "House was edited successfully!";
            //        return this.RedirectToAction("Details", "House", new { id });
        }

    }
}

