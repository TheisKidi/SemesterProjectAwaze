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


        // constructor 
        public CreateOrderModel(IGenericRepositoryService<Guest> guestService, IGenericRepositoryService<Property> propertyService)
        {
            _guestService = guestService;
            _propertyService = propertyService;
        }

        // methods
        public decimal CalculatePrice()
        {
            throw new Exception();
        }

        public void OnGet(string id)
        {
            SelectedProperty = _propertyService.GetById(id);
            GuestLoggedIn = _guestService.GetById(SessionHelper.GetProfile(HttpContext).Id);
        }

        public IActionResult OnPost()
        {
            throw new Exception();
        }
    }
}
