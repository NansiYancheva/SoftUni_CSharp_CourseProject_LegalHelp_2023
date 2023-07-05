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
        //ChangeStatus?
        //if LegalAdviseId != null - then...
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

                //return this.RedirectToAction("Add", "Ticket");
                return this.RedirectToAction("Index", "Home");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter)
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to edit!";

                //return this.RedirectToAction("Add", "Ticket");
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
           // return this.RedirectToAction("Details", "House", new { id });
            return this.RedirectToAction("Index", "Home");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool houseExists = await this.ticketService
                .ExistsByIdAsync(id);
            if (!houseExists)
            {
                this.TempData[ErrorMessage] = "Ticket with the provided id does not exist!";

                return this.RedirectToAction("All", "Ticket");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter)
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

                return this.RedirectToAction("Mine", "Ticket");
            }
            //if the category is request for document - there should not be a possibility to delete?
            //taking into consideration - if according to GDPR you could delete all your tickets, or only the one which are with status != resolved.

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

                return this.RedirectToAction("All", "Ticket");
            }

            string? userId = this.User.GetId();
            bool isReporter = await this.ticketService
                .IsUserReporterOfTheTicket(id, userId!);
            if (!isReporter)
            {
                this.TempData[ErrorMessage] = "You must be the reporter of the ticket you want to delete!";

                return this.RedirectToAction("Mine", "Ticket");
            }

            //if the category is request for document - there should not be a possibility to delete?
            //taking into consideration - if according to GDPR you could delete all your tickets, or only the one which are with status != resolved.
            try
            {
                await this.ticketService.DeleteTicketByIdAsync(id);

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
                myTickets.AddRange(await this.ticketService.AllByUserIdAsync(userId!));
              
                return this.View(myTickets);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        //Common
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }

}

