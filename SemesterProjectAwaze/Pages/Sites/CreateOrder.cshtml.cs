using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;
using System.Runtime.CompilerServices;

namespace SemesterProjectAwaze.Pages.Sites
{
    public class CreateOrderModel : PageModel
    {
        // instance field
        private IGenericRepositoryService<Guest> _guestService;
        private IGenericRepositoryService<Property> _propertyService;
        private OrderRepositoryServiceDB _orderService;

        // properties
        [BindProperty]
        public Guest GuestLoggedIn { get; set; }
        [BindProperty]
        public Property SelectedProperty { get; set; }
        public decimal Price { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        [BindProperty]
        public int CardNumber { get; set; }
        [BindProperty]
        public int Cvc { get; set; }
        [BindProperty]
        public int Month { get; set; }
        [BindProperty]
        public int Year { get; set; }


        // constructor 
        public CreateOrderModel(IGenericRepositoryService<Guest> guestService, 
            IGenericRepositoryService<Property> propertyService, OrderRepositoryServiceDB orderService)
        {
            _guestService = guestService;
            _propertyService = propertyService;
            _orderService = orderService;
        }

        // methods
        public decimal CalculatePrice()
        {
            var nights = StartDate - EndDate;
            decimal totalDays = Convert.ToDecimal(nights.TotalDays);

            Price = totalDays * SelectedProperty.PricePrNight;
            return Price;
        }

        public void OnGet(string id)
        {
            SelectedProperty = SelectedProperty = _propertyService.GetById(id);
            try
            {
                GuestLoggedIn = _guestService.GetById(SessionHelper.GetProfile(HttpContext).Id);
            }
            catch (Exception ex)
            {
                RedirectToPage("../Login/GuestLogin");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order newOrder = new Order(OrderId, GuestLoggedIn.MyBookingId, SelectedProperty.Id, 
                                       StartDate, EndDate, Price);
            _orderService.Create(newOrder);
            return RedirectToPage("Reciept");
        }
    }
}