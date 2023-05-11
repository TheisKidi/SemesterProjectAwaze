using AwazeLib.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjectAwaze.Services;

namespace SemesterProjectAwaze.Pages.Login
{
    public class GuestProfileModel : PageModel
    {
        private ILoginService _loginService;
        private IGenericRepositoryService<Property> _propertyService;
        private IGenericRepositoryService<Guest> _guestService;
        [BindProperty]
        public Guest Profile { get; set; }
        [BindProperty]
        public List<Property> Properties { get; set; }

        public GuestProfileModel(ILoginService loginService, IGenericRepositoryService<Property> propertyService, IGenericRepositoryService<Guest> guestService)
        {
            _loginService = loginService;
            _propertyService = propertyService;
            _guestService = guestService;
        }

        public void OnGet()
        {
            _loginService = SessionHelper.GetProfile(HttpContext);
            Properties = _propertyService.GetAll();
        }
    }
}
