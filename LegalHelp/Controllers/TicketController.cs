namespace LegalHelpSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LegalHelpSystem.Services.Data.Interfaces;
    using LegalHelpSystem.Web.ViewModels.Ticket;


    using static Common.NotificationMessagesConstants;

    public class TicketController : BaseController
    {
        public class HouseController : Controller
        {
            private readonly ITicketCategoryService ticketCategoryService;
            private readonly ITicketService ticketService;

            public HouseController(ITicketCategoryService _ticketCategoryService, ITicketService _ticketService)
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

                   await ticketService.AddTicketAsync(model);

                    this.TempData[SuccessMessage] = "Ticket was added successfully!";
                    return this.RedirectToAction("All", "Requests");
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
}
    
